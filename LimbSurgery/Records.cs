using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
namespace GnomoriaLimbSurgery.Records
{
    [Flags]
    public enum BodyPartStatus
    {
        Good = 0,
        Disabled = 1,
        Missing = 2,
        ExposesContainedParts = 4,
        Bleeding = 8,
        StruckArtery = 16,
        Poisoned = 32
    }
    [Flags]
    public enum BodySectionStatus
    {
        Good = 0,
        Destroyed = 1,
        Missing = 2,
        Bleeding = 4,
        StruckArtery = 8,
        InternalBleeding = 16
    }
    public enum HealthStatusAilment
    {
        Unconcious,
        Dazed,
        Faint,
        Winded,
        Dizzy,
        FallenOver,
        Grounded,
        Blind,
        ZombieVirus
    }
    public static class EnumConverts
    {
        public static BodyPartStatus Conv(this Game.BodyPartStatus el)
        {
            return (BodyPartStatus)el;
        }
        public static BodySectionStatus Conv(this Game.BodySectionStatus el)
        {
            return (BodySectionStatus)el;
        }
        public static HealthStatusAilment Conv(this Game.HealthStatusAilment el)
        {
            return (HealthStatusAilment)el;
        }
        public static Game.BodyPartStatus Conv(this BodyPartStatus el)
        {
            return (Game.BodyPartStatus)el;
        }
        public static Game.BodySectionStatus Conv(this BodySectionStatus el)
        {
            return (Game.BodySectionStatus)el;
        }
        public static Game.HealthStatusAilment Conv(this HealthStatusAilment el)
        {
            return (Game.HealthStatusAilment)el;
        }
    }
    public static class SerializationExtends
    {
        public static T GetValue<T>(this SerializationInfo self, string name, out T value)
        {
            return value = self.GetValue<T>(name);
        }
        public static T GetValue<T>(this SerializationInfo self, string name)
        {
            return (T)self.GetValue(name, typeof(T));
        }
        public static void AddValue<T>(this SerializationInfo self, string name, T value)
        {
            self.AddValue(name, value, typeof(T));
        }
    }
    [DataContract]
    public class Limb //: ISerializable
    {
        [DataMember]
        public int Index { get; private set; }
        [DataMember]
        public BodyPartStatus PartStatus { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public BodySectionStatus SectionStatus { get; private set; }
        public Limb(Game.BodySection section)
        {
            Index = section.Body.BodySections.IndexOf(section);
            Name = section.Name;
            PartStatus = section.BodyPart.Status.Conv();
            SectionStatus = section.Status.Conv();
        }
        /*protected Limb(SerializationInfo info, StreamingContext context)
        {
            Index = info.GetValue<int>("Index");
            PartStatus = info.GetValue<BodyPartStatus>("PartStatus");
            Name = info.GetValue<String>("Name");
            SectionStatus = info.GetValue<BodySectionStatus>("SectionStatus");
        }
        *//*void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Index", Index);
            info.AddValue("PartStatus", PartStatus);
            info.AddValue("Name", Name);
            info.AddValue("SectionStatus", SectionStatus);
        }*/
    }
/*    [Serializable]
    [KnownType(typeof(Limb[]))]
    [KnownType(typeof(HealthStatusAilment[]))]
    */
    [DataContract]
    public class Patient //: ISerializable
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Profession { get; private set; }
        public IEnumerable<Limb> Parts
        {
            get
            {
                return mParts;
            }
        }
        [DataMember]
        private Limb[] mParts;
        [DataMember]
        private HealthStatusAilment[] mEffects;
        public IEnumerable<HealthStatusAilment> Effects
        {
            get
            {
                return mEffects;
            }
        }

        public Patient Get_Clear()
        {
            return new Patient(this) { mParts = new Limb[0], mEffects = new HealthStatusAilment[0] };
        }
        public Patient Get_ToggleLimb(Limb limbToToggle)
        {
            var exists = mParts.Any(el=>el.Name == limbToToggle.Name);
            return new Patient(this, limbs: exists ? mParts.Where(el => el.Name != limbToToggle.Name).ToArray() : mParts.Union(limbToToggle).ToArray());
        }
        public Patient Get_ToggleEffect(HealthStatusAilment effectToToggle)
        {
            var exists = mEffects.Contains(effectToToggle);
            return new Patient(this, effects: exists ? mEffects.Where(el => el != effectToToggle).ToArray() : mEffects.Union(effectToToggle).ToArray());
        }
        public Patient(Patient from, IEnumerable<Limb> limbs = null, IEnumerable<HealthStatusAilment> effects = null)
        {
            Name = from.Name;
            Profession = from.Profession;
            mParts = (limbs ?? from.mParts).ToArray();
            mEffects = (effects ?? from.mEffects).ToArray();
        }
        public Patient(Game.Character chr, IEnumerable<Limb> parts)
        {
            mEffects = chr.Body.StatusEffects.Select(kvp => kvp.Key.Conv()).ToArray();
            Name = chr.Name();
            Profession = chr.Mind.Profession.Title;
            mParts = parts.ToArray();
        }
        /*
        protected Patient(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            Profession = info.GetString("Profession");
            info.GetValue("Parts", out mParts);
            info.GetValue("Effects", out mEffects);
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Profession", Profession);
            info.AddValue("Parts", mParts);
            info.AddValue("Effects", mEffects);
        }*/
        public override string ToString()
        {
            return Name + " (" + Profession + ")";
        }
        public bool HasAny()
        {
            return mParts.Length + mEffects.Length > 0;
        }
    }
}
