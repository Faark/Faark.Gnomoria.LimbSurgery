using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game;
using GnomoriaLimbSurgery.Records;

namespace GnomoriaLimbSurgery
{
    public class Worker : MarshalByRefObject
    {
        private GnomanEmpire gnomoria_game;
        private string gnomoria_path;
        private bool try_extractRelativePath(string full_path, string relative_based_on, out string result)
        {
            result = Uri.UnescapeDataString(
                new Uri(relative_based_on)
                    .MakeRelativeUri(new Uri(full_path))
                    .ToString()
                    .Replace('/', System.IO.Path.DirectorySeparatorChar)
                );
            return !(result.Contains('/') || result.Contains('\\'));
        }


        public string save_directory;

        public void init_gnomoria(string gnomoria_directory)
        {
            gnomoria_path = gnomoria_directory;
            gnomoria_game = GnomanEmpire.Instance;
            gnomoria_game.Content.RootDirectory = System.IO.Path.Combine(gnomoria_path, "Content");
            System.IO.Directory.SetCurrentDirectory(gnomoria_path);
            typeof(GnomanEmpire).GetMethod("Initialize", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(gnomoria_game, null);
            save_directory = GnomanEmpire.SaveFolderPath("Worlds\\");
        }
        public void init_7z()
        {
            SevenZip.SevenZipCompressor.SetLibraryPath(System.IO.Path.Combine(gnomoria_path, "7z.dll"));
        }
        public void load(string filename)
        {
            var gnomoria_save_path = GnomanEmpire.SaveFolderPath("Worlds\\");
            string world_save_file;
            if (!try_extractRelativePath(filename, gnomoria_save_path, out world_save_file))
            {
                throw new Exception("Error: Can only open saves in your Gnomoria worlds save folder!");
            }
            System.IO.Directory.SetCurrentDirectory(gnomoria_path);
            gnomoria_game.LoadGame(world_save_file, false);
        }
        public string getMedicalRecords()
        {
            return Serialization.JSON.ToJSON(gnomoria_game.World.AIDirector.PlayerFaction.Members
                .Select(kvp => new
                {
                    c = kvp.Value,
                    sec = kvp.Value.Body.BodySections
                        .Where(sec => sec.Status != Game.BodySectionStatus.Good || sec.BodyPart.Status != Game.BodyPartStatus.Good)
                        .Select(sec => new Limb(sec))
                })
                .Where(data => !data.c.IsHealthy() || data.sec.Count() > 0)
                .Select(data => new Patient(data.c, data.sec))
                .ToArray());
        }
        private void applyAprilFool(IEnumerable<Character> chrs)
        {
            foreach (var c in chrs)
            {
                typeof(Body)
                    .GetField("ce0713b9a150d71be2238dd015badef00", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    .SetValue(c.Body, 600f * GnomanEmpire.Instance.RandomInRange(5, 7));
            }
        }

        private static System.Reflection.FieldInfo bloodLossRate = typeof(Body).GetField("ca1e33315182933f3194afd5d29fb631e", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        private static System.Reflection.FieldInfo bloodLevel = typeof(Body).GetField("cd65dc5c565821905fa71a3b3d2eebdf1", System.Reflection.BindingFlags.Instance| System.Reflection.BindingFlags.NonPublic);
        private static System.Reflection.FieldInfo bodyPartProperty = typeof(BodyPart)
            .GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .Where(field => (field.FieldType == typeof(Game.BodyPartStatus)))
            .Single();
        private static System.Reflection.FieldInfo bodySectionProperty = typeof(BodySection)
            .GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .Where(field => (field.FieldType == typeof(Game.BodySectionStatus)))
            .Single();
        private void treat_postProcessGnome(Character gnome)
        {
            bloodLossRate.SetValue(gnome.Body, gnome.Body.BodySections.Sum(sec => sec.BloodLossRate));
            bloodLevel.SetValue(gnome.Body, 100);
        }
        private void treat_limb(BodySection section)
        {
            if (section.Status == Game.BodySectionStatus.Missing)
            {
                bodySectionProperty.SetValue(section, Game.BodySectionStatus.Destroyed);
                bodyPartProperty.SetValue(section.BodyPart, Game.BodyPartStatus.Disabled);
            }
            section.RepairDestroyedBodySection();
            bodySectionProperty.SetValue(section, Game.BodySectionStatus.Good);
            bodyPartProperty.SetValue(section.BodyPart, Game.BodyPartStatus.Good);
        }
        public Tuple<string, string>[] healSelected(string records_txt, bool doFool)
        {
            var records = Serialization.JSON.FromJSON<Patient[]>(records_txt);
            var treatedGnomes = gnomoria_game.World.AIDirector.PlayerFaction.Members
                .Select(list_el => new
                {
                    Record = records.FirstOrDefault(el => el.Name == list_el.Value.Name()),
                    Gnome = list_el.Value
                }).Where(el => el.Record != null).ToList();
            var healedLimbs = treatedGnomes.SelectMany(rec =>
            {
                return rec.Gnome.Body.BodySections.Select(sect => new
                {
                    rec = rec,
                    sectRec = rec.Record.Parts.FirstOrDefault(part => part.Index == rec.Gnome.Body.BodySections.IndexOf(sect)),
                    sect = sect
                })
                .Where(data => data.sectRec != null);
            }).ToList().Select(data =>
            {
                treat_limb(data.sect);
                return new { gnome = data.rec.Gnome, cured = data.sect.Name };
            });

            var healedEffects = treatedGnomes.SelectMany(rec =>
            {
                return rec.Gnome.Body.StatusEffects.Where(effect => rec.Record.Effects.Contains(effect.Key.Conv())).Select(effect => new { rec = rec, effect = effect.Key });
            }).ToList().Select(data =>
            {
                data.rec.Gnome.Body.StatusEffects.Remove(data.effect);
                return new { gnome = data.rec.Gnome, cured = data.effect.ToString() };
            });

            var allTreated = healedLimbs.Union(healedEffects).GroupBy(el=>el.gnome).ToList();

            if (doFool)
                applyAprilFool(allTreated.Select(group => group.Key));

            return allTreated.Select(group => {
                treat_postProcessGnome(group.Key);
                
                return Tuple.Create(
                    group.Key.Name(),
                    String.Join(", ",group.Select(a=>a.cured))
                    );
            }).ToArray();
        }
        public Tuple<string, string>[] healAll(bool doFool)
        {
            var healed = gnomoria_game.World.AIDirector.PlayerFaction.Members
                .Select(list_el => list_el.Value)
                .SelectMany(gnom => gnom.Body.BodySections)
                .Where(body_section => body_section.Status == Game.BodySectionStatus.Missing)
                .ToList()
                .Select(body_section =>
                {
                    treat_limb(body_section);
                    return body_section;
                })
                .GroupBy(body_section => body_section.Body.Character)
                .ToList();
            if (doFool)
                applyAprilFool(healed.Select(el => el.Key.Body.Character));
            return healed.Select(grouped_body_selection =>
               {
                   grouped_body_selection.Key.Body.StatusEffects.Clear();
                   treat_postProcessGnome(grouped_body_selection.Key);
                   return Tuple.Create(
                       grouped_body_selection.Key.Name(),
                       grouped_body_selection
                               .Select(body_selection => body_selection.Name)
                               .Aggregate((s1, s2) => s1 + ", " + s2)
                   );
               }).ToArray();

        }
        public void save()
        {
            gnomoria_game.SaveGame().Wait();
        }
        public void reset()
        {
        }
    }
}
