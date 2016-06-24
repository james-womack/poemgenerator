using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoetryEngine {
    class PoemDictionary {

        private string fileName = "dictionary.json";
        private string folderName = "data";
        private List<Word> Words = null;

        public PoemDictionary() {
            Load();
        }

        public async void Load() {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            if((await folder.CheckExistsAsync(fileName)) == ExistenceCheckResult.FileExists) {
                IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                Words = JsonConvert.DeserializeObject<List<Word>>(await file.ReadAllTextAsync());
            } else
                Words = new List<Word>() {
                    new Word("snow flake", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "snow flakes", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("sun", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "suns", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("water", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "waters", Type = GrammerRuleType.Pleral }
                        },
                        GrammerExeptions = new List<GrammerException>() {
                            new GrammerException() { Type = GrammerExceptionType.NoAnOrA }
                        } },
                    new Word("cloud", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "clouds", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("electricity", WordType.Object){
                        GrammerExeptions = new List<GrammerException> {
                            new GrammerException() { Type = GrammerExceptionType.NoPleral },
                            new GrammerException() { Type = GrammerExceptionType.NoAnOrA }
                        } },
                    new Word("star", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "stars", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("wind", WordType.Object) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "winds" ,Type = GrammerRuleType.Pleral }
                        },
                        GrammerExeptions = new List<GrammerException>() {
                            new GrammerException() { Type = GrammerExceptionType.NoAnOrA }
                        } },
                    new Word("tree", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "trees", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("leaf", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "leaves", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("mouse", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "mice", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("puppy", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "puppies", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("moon", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "moons", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("night", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "nights", Type = GrammerRuleType.Pleral }
                        },
                        GrammerExeptions = new List<GrammerException>() {
                            new GrammerException() { Type = GrammerExceptionType.NoAnOrA }
                        } },
                    new Word("child", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "children", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("path", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "paths", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("vessel", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "vessels", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("blood", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "drops of blood", Type = GrammerRuleType.Pleral }
                        },
                        GrammerExeptions = new List<GrammerException>() {
                            new GrammerException() { Type = GrammerExceptionType.NoAnOrA }
                        } },
                    new Word("mind", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "minds", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("rain", WordType.Object) {
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "rain drops", Type = GrammerRuleType.Pleral }
                        },
                        GrammerExeptions = new List<GrammerException>() {
                            new GrammerException() { Type = GrammerExceptionType.NoAnOrA }
                        } },
                    new Word("field", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "fields", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("heresy", WordType.Object){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "heresies", Type = GrammerRuleType.Pleral }
                        } },
                    new Word("flow", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "flowing", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "flowed", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("construct", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "constructing", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "constructed", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("run", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "running", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "ran", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("fly", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "flying", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "flew", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("roll", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "rolling", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "rolled", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("fall", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "falling", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "fell", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("fight", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "fighting", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "fought", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("soar", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "soaring", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "soared", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("climb", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "climbing", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "climbed", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("ascend", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "ascending", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "ascended", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("spiral", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "spiraling", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "spiraled", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("drift", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "drifting", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "drifted", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("glide", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "gliding", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "glided", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("rise", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "rising", Type = GrammerRuleType.ING},
                            new GrammerRule() { Data = "rose", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("hover", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "hovering", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "hovered", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("wander", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "wandering", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "wandered", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("stroll", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "strolling", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "strolled", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("search", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "searching", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "searched", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("travel", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "traveling", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "traveled", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("sit", WordType.Movment){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "sitting", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "sat", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("is like", WordType.Transition){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "are like", Type = GrammerRuleType.Pleral },
                            new GrammerRule() { Data = "will be like", Type = GrammerRuleType.FutureTense }
                        } },
                    new Word("is", WordType.Transition){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "are", Type = GrammerRuleType.Pleral },
                            new GrammerRule() { Data = "will be", Type = GrammerRuleType.FutureTense }
                        } },
                    new Word("is reminiscent of", WordType.Transition){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "are reminiscent of", Type = GrammerRuleType.Pleral },
                            new GrammerRule() { Data = "will be reminiscent of", Type = GrammerRuleType.FutureTense }
                        } },
                    new Word("is as", WordType.Transition){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "are as", Type = GrammerRuleType.Pleral },
                            new GrammerRule() { Data = "will be as", Type = GrammerRuleType.FutureTense }
                        } },
                    new Word("is fighting", WordType.Transition){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "are fighting", Type = GrammerRuleType.Pleral },
                            new GrammerRule() { Data = "will fight", Type = GrammerRuleType.FutureTense }
                        } },
                    new Word("opposes", WordType.Transition){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "oppose", Type = GrammerRuleType.Pleral },
                            new GrammerRule() { Data = "will oppose", Type = GrammerRuleType.FutureTense }
                        } },
                    new Word("through", WordType.Prepasition),
                    new Word("with", WordType.Prepasition),
                    new Word("through out", WordType.Prepasition),
                    new Word("in", WordType.Prepasition),
                    new Word("above", WordType.Prepasition),
                    new Word("under", WordType.Prepasition),
                    new Word("beside", WordType.Prepasition),
                    new Word("against", WordType.Prepasition),
                    new Word("river", WordType.Expance),
                    new Word("void", WordType.Expance),
                    new Word("storm", WordType.Expance),
                    new Word("canyon", WordType.Expance),
                    new Word("ocean", WordType.Expance),
                    new Word("galaxy", WordType.Expance),
                    new Word("forest", WordType.Expance),
                    new Word("time", WordType.Expance) {
                        GrammerExeptions = new List<GrammerException>() {
                            new GrammerException() { Type = GrammerExceptionType.NoAnOrA }
                        } },
                    new Word("abyss", WordType.Expance),
                    new Word("sky", WordType.Expance),
                    new Word("air", WordType.Expance) {
                        GrammerExeptions = new List<GrammerException>() {
                            new GrammerException() { Type = GrammerExceptionType.NoAnOrA }
                        } },
                    new Word("timeless", WordType.Deepener),
                    new Word("fruitful", WordType.Deepener),
                    new Word("meaningless", WordType.Deepener),
                    new Word("lonesome", WordType.Deepener),
                    new Word("restless", WordType.Deepener),
                    new Word("evil", WordType.Deepener),
                    new Word("chaotic", WordType.Deepener),
                    new Word("glorious", WordType.Deepener),
                    new Word("everlasting", WordType.Deepener),
                    new Word("thought provoking", WordType.Deepener),
                    new Word("shake", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "shaking", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "shook", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("brake", WordType.Action) {
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "braking", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "broke", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("punish", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "punishing", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "punished", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("crush", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "crushing", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "crushed", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("eliminate", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "eliminating", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "eliminated", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("speed", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "speeding", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "sped", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("destroy", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "destroying", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "destroyed", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("find", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "finding", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "found", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("save", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "saving", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "saved", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("restore", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "restoring", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "restored", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("kill", WordType.Action){
                        GrammerRules = new List<GrammerRule> {
                            new GrammerRule() { Data = "killing", Type = GrammerRuleType.ING },
                            new GrammerRule() { Data = "killed", Type = GrammerRuleType.PastTense }
                        } },
                    new Word("time", WordType.Thought),
                    new Word("brotherhood", WordType.Thought),
                    new Word("sisterhood", WordType.Thought),
                    new Word("trust", WordType.Thought),
                    new Word("love", WordType.Thought),
                    new Word("hate", WordType.Thought),
                    new Word("fate", WordType.Thought),
                    new Word("lust", WordType.Thought),
                    new Word("peace", WordType.Thought),
                    new Word("war", WordType.Thought),
                    new Word("pain", WordType.Thought),
                    new Word("death", WordType.Thought),
                    new Word("life", WordType.Thought),
                    new Word("playful", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "playfully", Type = GrammerRuleType.LY},
                        } },
                    new Word("electric", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "electricly", Type = GrammerRuleType.LY},
                        } },
                    new Word("distasteful", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "distastefully", Type = GrammerRuleType.LY},
                        } },
                    new Word("pretentious", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "pretentiously", Type = GrammerRuleType.LY},
                        } },
                    new Word("crazy", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "crazily", Type = GrammerRuleType.LY},
                        } },
                    new Word("harmonious", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "harmoniously", Type = GrammerRuleType.LY},
                        } },
                    new Word("blissful", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "blissfully", Type = GrammerRuleType.LY},
                        } },
                    new Word("swift", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "swiftly", Type = GrammerRuleType.LY }
                        } },
                    new Word("ignorant", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "ignorantly", Type = GrammerRuleType.LY }
                        } },
                    new Word("swift", WordType.Adjective) {
                        GrammerRules = new List<GrammerRule>() {
                            new GrammerRule() { Data = "swiftly", Type = GrammerRuleType.LY }
                        } },
                    new Word ("five", WordType.Amount),
                    new Word ("thirty-five", WordType.Amount),
                    new Word ("sixteen", WordType.Amount),
                    new Word ("ninty", WordType.Amount),
                    new Word ("seven", WordType.Amount),
                    new Word ("six", WordType.Amount),
                    new Word ("two", WordType.Amount),
                    new Word ("three", WordType.Amount),
                    new Word ("he", WordType.Pronoun),
                    new Word ("she", WordType.Pronoun),
                    new Word ("it", WordType.Pronoun),
                    new Word ("you", WordType.Pronoun),
                    new Word ("everyone", WordType.Pronoun),
                    new Word ("no one", WordType.Pronoun),
                    new Word ("they", WordType.Pronoun),
                    new Word ("i", WordType.Pronoun),
                    new Word ("we", WordType.Pronoun),
                    new Word ("down", WordType.Direction),
                    new Word ("left", WordType.Direction),
                    new Word ("right", WordType.Direction),
                    new Word ("up", WordType.Direction),
                    new Word ("toward the mountain", WordType.Direction),
                    new Word ("toward the sea", WordType.Direction),
                    new Word ("toward the sun", WordType.Direction),
                    new Word ("toward the moon", WordType.Direction),
                };
        }

        public async void Save() {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(JsonConvert.SerializeObject(Words, Formatting.Indented));
            System.IO.File.WriteAllText(fileName, JsonConvert.SerializeObject(Words, Formatting.Indented));
        }

        public List<Word> GetWords() {
            return Words;
        }

        public List<Word> GetWordsOfType(WordType type) {
            return Words.Where(x => x.Type == type).ToList();
        }
    }
}
