using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PCLStorage;

namespace PoetryEngine {
    public class Generator {

        private static string fileName = "structures.json";
        private static string folderName = "data";
        private static Random rand = new Random();
        private static PoemDictionary dic = new PoemDictionary();
        private static List<PoemStructure> structures;
        public static List<char> punc = new List<char> { '.', ',', ';', '?', '!'};

        public static Dictionary<Word, double> used = new Dictionary<Word, double>();
        public static HistoryKeeper<Tuple<Word, GrammerRuleType[]>> history = new HistoryKeeper<Tuple<Word, GrammerRuleType[]>>();
        public static List<int> usedStructs = new List<int>();

        static Dictionary<string, Func<string, Tuple<Word, GrammerRuleType[]>>> funcMap = new Dictionary<string, Func<string, Tuple<Word, GrammerRuleType[]>>>
        {
            {"tn", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Transition)), new GrammerRuleType[] { })},
            {"tnc", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Transition)), new GrammerRuleType[] {GrammerRuleType.ConsiderContext})},
            {"tnp", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Transition)), new GrammerRuleType[] {GrammerRuleType.Plural})},
            {"tnf", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Transition)), new GrammerRuleType[] {GrammerRuleType.FutureTense})},
            {"nnp", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Object)), new GrammerRuleType[] {GrammerRuleType.Plural}) },
            {"nn", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Object)), new GrammerRuleType[] { })},
            {"nna", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Object)), new GrammerRuleType[] { GrammerRuleType.AddAOrAn})},
            {"pr", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Prepasition)), new GrammerRuleType[] { })},
            {"mvi", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Movment)), new GrammerRuleType[] {GrammerRuleType.ING})},
            {"mva", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Movment)), new GrammerRuleType[] {GrammerRuleType.ING, GrammerRuleType.AddAOrAn})},
            {"mvp", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Movment)), new GrammerRuleType[] {GrammerRuleType.PastTense })},
            {"ep", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Expance)), new GrammerRuleType[] { })},
            {"epa", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Expance)), new GrammerRuleType[] { GrammerRuleType.AddAOrAn})},
            {"epo", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Expance)), new GrammerRuleType[] {GrammerRuleType.Owning })},
            {"dp", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Deepener)), new GrammerRuleType[] { })},
            {"dpa", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Deepener)), new GrammerRuleType[] {GrammerRuleType.AddAOrAn })},
            {"ati", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Action)), new GrammerRuleType[] {GrammerRuleType.ING })},
            {"th", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Thought)), new GrammerRuleType[] { })},
            {"am", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Amount)), new GrammerRuleType[] { })},
            {"ad", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Adjective)), new GrammerRuleType[] { })},
            {"adl", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Adjective)), new GrammerRuleType[] { GrammerRuleType.LY})},
            {"dr", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Direction)), new GrammerRuleType[] { })},
            {"pn", s => new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(WordType.Pronoun)), new GrammerRuleType[] { })}
        };

        public static string Gen() {
            Random random = new Random();
            int amount = random.Next(3,7);
            var poemB = new StringBuilder();

            for(int a = 0; a < amount; ++a) {
#if DEBUG
                var structureparts = GetStructure();
                poemB.Append("s" + structureparts.Item2 + ": ");
                var structure = structureparts.Item1;
#else
                var structure = GetStructure();
#endif

                foreach(var part in structure.Parts) {
                    if(part.Item1.Type != WordType.Word) {
                        history.AddHistory(new Tuple<Word, GrammerRuleType[]>(PickRandomWithWeightFromUsed(dic.GetWordsOfType(part.Item1.Type)), part.Item2));
                    }
                }

                for(int i = 0; i < structure.Parts.Count(); ++i) {
                    if(structure.Parts[i].Item1.Type != WordType.Word) {
                        var currentWord = history.GetCurrentElement();
                        poemB.Append(currentWord.Item1.GetWord(currentWord.Item2));
                        history.TraverceCurrentElement();
                    } else
                        poemB.Append(structure.Parts[i].Item1.Base);
                    if(structure.Parts.Count() > i + 1 && (structure.Parts[i + 1].Item1.Base == null || !punc.Contains(structure.Parts[i + 1].Item1.Base.Last()))) {
                        poemB.Append(" ");
                    }
                }

                history.ClearHistory();
                poemB.AppendLine();
            }
            
            used.Clear();
            return poemB.ToString();
        }

        public static Word PickRandomWithWeightFromUsed(List<Word> words) {
            double total = 0.0D;
            foreach(var w in words) {
                if(!used.ContainsKey(w))
                    used.Add(w, 100.0D);
                total += used.FirstOrDefault(x => x.Key == w).Value;
            }
            double r = rand.NextDouble() * total;
            foreach(var w in words) {
                if(r <= used.FirstOrDefault(x => x.Key == w).Value) {
                    var temp = used.FirstOrDefault(x => x.Key == w).Value;
                    temp *= 0.25;//TODO if weight isnt being set right this is why
                    used.Remove(w);
                    used.Add(w, temp);
                    return w;
                }
                r -= used.FirstOrDefault(x => x.Key == w).Value;
            }
            return new Word("ERROR", words.First().Type);
        }

        public static Tuple<Word, GrammerRuleType[]> GetLastWordUsed() {
            return history.GetPreviousElement();
        }

        public static void AddToHistory(Word word, GrammerRuleType[] rules) {
            history.AddHistory(new Tuple<Word, GrammerRuleType[]>(word, rules));
        }

        public static async void Load() {
            dic.Load();
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            if((await folder.CheckExistsAsync(fileName)) == ExistenceCheckResult.FileExists) {
                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                //structures = JsonConvert.DeserializeObject<List<string>>(await file.ReadAllTextAsync());
            } else {
                structures = new List<PoemStructure> {
                    new PoemStructure("%nnp %tnc the %nnp %mvi %pr the %ep like the %dp %ep %pr the %epo %nn %ati the %nnp of %th ."),
                    new PoemStructure("%nn %tn the %nnp %mvi %pr the %ad %ep like the %dp %ep %pr the %ep %ati the %nn of %th ."),
                    new PoemStructure("the %nnp , the %nnp , and the %nnp %mvp %pr the %ad %ep and these %tnp the %nnp %ati %nnp and %ad %nnp . It %tn the %ep ."),
                    new PoemStructure("%am %nnp %mvp %pr %ad %nnp ."),
                    new PoemStructure("%am %nnp %mvp %dr as they could have %mvp ."),
                    new PoemStructure("because it %tnf the %ad and %ad %nn ."),
                    new PoemStructure("%mvi , %mva %nn %tn %dpa %nn %ati the %ad %nn ."),
                    new PoemStructure("And %pn , the %nn , %pr %ad %nn ."),
                    new PoemStructure("%nna %tn %nnp %mvi %pr %epa %adl .")
                };
            }
        }

#if DEBUG
        public static Tuple<PoemStructure, int> GetStructure() {
#else
        public static PoemStructure GetStructure() {
#endif
            double[] weights = new double[structures.Count()];
            double total = 0.0D;
            for(int i = 0; i < weights.Length; ++i) {
                weights[i] = 100 * Math.Pow(0.5, usedStructs.Count(a => a == i));
                total += weights[i];
            }
            var r = rand.NextDouble() * total;
            for(int i = 0; i < weights.Length; ++i) {
                if(r < weights[i]) {
                    usedStructs.Add(i);
#if DEBUG
                    return new Tuple<PoemStructure, int>(structures[i], i);
#else
                    return structures[i];
#endif
                }
                r -= weights[i];
            }
#if DEBUG
            return new Tuple<PoemStructure, int>(new PoemStructure("%pn %tn %nn ."), -1);
#else
            return new PoemStructure("%pn %tn %nn .");
#endif
        }
        
        public static async void Save() {
            dic.Save();
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(JsonConvert.SerializeObject(structures, Formatting.Indented));
        }
    }
}
