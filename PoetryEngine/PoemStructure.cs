using PoetryEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoetryEngine {
    [Serializable()]
    public class PoemStructure : ISerializable {
        static Dictionary<string, Tuple<PoemStructurePart, GrammerRuleType[]>> Map = new Dictionary<string, Tuple<PoemStructurePart, GrammerRuleType[]>>
        {
            {"tn", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Transition}, new GrammerRuleType[] { }) },
            {"tnc", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Transition }, new GrammerRuleType[] {GrammerRuleType.ConsiderContext})},
            {"tnp", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Transition }, new GrammerRuleType[] {GrammerRuleType.Plural})},
            {"tnf", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Transition }, new GrammerRuleType[] {GrammerRuleType.FutureTense})},
            {"nnp", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Object }, new GrammerRuleType[] {GrammerRuleType.Plural}) },
            {"nn", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Object }, new GrammerRuleType[] { })},
            {"nna", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Object }, new GrammerRuleType[] { GrammerRuleType.AddAOrAn})},
            {"pr", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Prepasition }, new GrammerRuleType[] { })},
            {"mvi", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Movment }, new GrammerRuleType[] {GrammerRuleType.ING})},
            {"mva", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Movment }, new GrammerRuleType[] {GrammerRuleType.ING, GrammerRuleType.AddAOrAn})},
            {"mvp", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Movment }, new GrammerRuleType[] {GrammerRuleType.PastTense })},
            {"ep", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Expance }, new GrammerRuleType[] { })},
            {"epa", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Expance }, new GrammerRuleType[] { GrammerRuleType.AddAOrAn})},
            {"epo", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Expance }, new GrammerRuleType[] {GrammerRuleType.Owning })},
            {"dp", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Deepener }, new GrammerRuleType[] { })},
            {"dpa", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Deepener }, new GrammerRuleType[] {GrammerRuleType.AddAOrAn })},
            {"ati", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Action }, new GrammerRuleType[] {GrammerRuleType.ING })},
            {"th", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Thought }, new GrammerRuleType[] { })},
            {"am", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Amount }, new GrammerRuleType[] { })},
            {"ad", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Adjective }, new GrammerRuleType[] { })},
            {"adl", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Adjective }, new GrammerRuleType[] { GrammerRuleType.LY})},
            {"dr", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Direction }, new GrammerRuleType[] { })},
            {"pn", new Tuple<PoemStructurePart, GrammerRuleType[]>(new PoemStructurePart() { Type = WordType.Pronoun }, new GrammerRuleType[] { })}
        };

        private string _id = "";
        public string Id { get { return _id; } set { if(string.IsNullOrWhiteSpace(_id)) _id = value; } }
        public List<Tuple<PoemStructurePart, GrammerRuleType[]>> Parts { get; set; }
        public List<string> Followers = null;

        public PoemStructure(string s) {
            Parts = new List<Tuple<PoemStructurePart, GrammerRuleType[]>>();
            var structureParts = s.Split(null as char[]);

            foreach(var part in structureParts) {
                if(!string.IsNullOrWhiteSpace(part) && part[0] == '%') {
                    Parts.Add(Map[part.Substring(1)]);
                } else {
                    Parts.Add(new Tuple<PoemStructurePart, GrammerRuleType[]>(
                        new PoemStructurePart() { Type = WordType.Word, Base = part.Trim() }, new GrammerRuleType[] { }));
                }
            }
        }

        public PoemStructure(string s, string id) : this(s) {
            Id = id;
        }

        public PoemStructure(string s, string id, List<string> followers) {
            Followers = followers;
        }

        public PoemStructure(SerializationInfo info, StreamingContext context) {
            Parts = (List<Tuple<PoemStructurePart, GrammerRuleType[]>>) info.GetValue("parts", typeof(List<Tuple<PoemStructurePart, GrammerRuleType[]>>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            info.AddValue("parts", Parts);
        }
    }

    public class PoemStructurePart {
        public WordType Type { get; set; }

        public string Base { get; set; }

        public static bool operator ==(PoemStructurePart w1, PoemStructurePart w2) {
            return w1.Base == w2.Base;
        }

        public static bool operator !=(PoemStructurePart w1, PoemStructurePart w2) {
            return w1.Base != w2.Base;
        }

        public override bool Equals(Object o) {
            if(o.GetType() == typeof(PoemStructurePart)) {
                var n = o as PoemStructurePart;
                return n == this;
            }
            return false;
        }

        public override int GetHashCode() {
            return Type.GetHashCode() * Base.GetHashCode();
        }
    }
}
