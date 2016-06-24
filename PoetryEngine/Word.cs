using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoetryEngine {
    public class Word {

        public string Base { get; set; }
        public WordType Type { get; set; }
        public List<GrammerRule> GrammerRules { get; set; }
        public List<GrammerException> GrammerExeptions { get; set; }

        public Word(string Base, WordType Type) {
            GrammerRules = new List<GrammerRule>();
            GrammerExeptions = new List<GrammerException>();

            if(!string.IsNullOrWhiteSpace(Base))
                this.Base = Base;
            else
                this.Base = "Empty";

            this.Type = Type;
        }

        /// <summary>
        /// returns a if the word should
        /// </summary>
        /// <returns></returns>
        public string GetAnOrA() {
            if(Base[0].IsVowel() || GrammerExeptions.Any(x => x.Type == GrammerExceptionType.UseAn))
                return "an";
            return "a";
        }

        public string GetWord(params GrammerRuleType[] Rules) {
            string pre = "";
            string word = Base;
            string post = "";

            if(Rules.Contains(GrammerRuleType.PreOwnership) && GrammerRules.Any(a => a.Type == GrammerRuleType.PreOwnership))
                pre += GrammerRules.FirstOrDefault(a => a.Type == GrammerRuleType.PreOwnership).Data + " ";

            if(Rules.Contains(GrammerRuleType.AddAOrAn) && !GrammerExeptions.Any(a => a.Type == GrammerExceptionType.NoAnOrA))
                pre += GetAnOrA() + " ";

            if(Rules.Contains(GrammerRuleType.Owning)) {
                if(Rules.Contains(GrammerRuleType.Pleral))
                    post += "s'";
                else
                    post += "'s";
            }

            if(Rules.Contains(GrammerRuleType.ER) && GrammerRules.Any(x => x.Type == GrammerRuleType.ER))
                word = GrammerRules.FirstOrDefault(x => x.Type == GrammerRuleType.ER).Data;
            else if(Rules.Contains(GrammerRuleType.ING) && GrammerRules.Any(x => x.Type == GrammerRuleType.ING))
                word = GrammerRules.FirstOrDefault(x => x.Type == GrammerRuleType.ING).Data;
            else if(Rules.Contains(GrammerRuleType.Pleral) && GrammerRules.Any(x => x.Type == GrammerRuleType.Pleral))
                word = GrammerRules.FirstOrDefault(x => x.Type == GrammerRuleType.Pleral).Data;
            else if(Rules.Contains(GrammerRuleType.PastTense) && GrammerRules.Any(x => x.Type == GrammerRuleType.PastTense))
                word = GrammerRules.FirstOrDefault(x => x.Type == GrammerRuleType.PastTense).Data;
            else if(Rules.Contains(GrammerRuleType.FutureTense) && GrammerRules.Any(x => x.Type == GrammerRuleType.FutureTense))
                word = GrammerRules.FirstOrDefault(x => x.Type == GrammerRuleType.FutureTense).Data;
            else if(Rules.Contains(GrammerRuleType.LY) && GrammerRules.Any(x => x.Type == GrammerRuleType.LY))
                word = GrammerRules.FirstOrDefault(x => x.Type == GrammerRuleType.LY).Data;
            else if(Rules.Contains(GrammerRuleType.ConsiderContext) && GrammerRules.Any(x => x.Type == GrammerRuleType.Pleral) 
                && Type == WordType.Transition && Generator.GetLastWordUsed() != null 
                && Generator.GetLastWordUsed().Item2.Contains(GrammerRuleType.Pleral) 
                && !Generator.GetLastWordUsed().Item1.GrammerExeptions.Any(a => a.Type == GrammerExceptionType.NoPleral))
                word = GrammerRules.FirstOrDefault(x => x.Type == GrammerRuleType.Pleral).Data;

            Generator.AddToHistory(this, Rules);

            return pre + word + post;
        }

        public static bool operator ==(Word w1, Word w2){
            return w1.Base == w2.Base;
        }

        public static bool operator !=(Word w1, Word w2) {
            return w1.Base != w2.Base;
        }
    }

    public class GrammerRule {
        public GrammerRuleType Type { get; set; }
        public string Data { get; set; }
    }

    public class GrammerException {
        public GrammerExceptionType Type { get; set; }
        public string Data { get; set; }
    } 

    public enum GrammerExceptionType {
        UseAn,
        NoPleral,
        NoAnOrA
    }

    public enum WordType {
        Object,
        Movment,
        Adjective,
        Transition,
        Prepasition,
        Expance,
        Deepener,
        Thought,
        Action,
        Amount,
        Pronoun,
        Direction
    }

    public enum GrammerRuleType {
        ER,
        ING,
        Pleral,
        PreOwnership,
        AddAOrAn,
        Owning,
        PastTense,
        FutureTense,
        ConsiderContext,
        LY
    }
}
