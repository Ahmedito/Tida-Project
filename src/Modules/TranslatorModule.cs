using Discord;
using Discord.Interactions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tida.Modules
{
    public class TranslatorModule : InteractionModuleBase<SocketInteractionContext>
    {
        public static Dictionary<string, string> words = new Dictionary<string, string>();

        [SlashCommand("addword", "wordPolish;wordGerman")]
        public async Task Addword(string text)
        {
            string[] split = text.Split(';');

            if (split.Length < 2)
            {
                await RespondAsync("You provided less than two words. Not. Cool. Dudes.");
                return;
            }
            if (split.Length > 2)
            {
                await RespondAsync("You provided more than two words. That is not how this bot works,,,, Bye bye! :shushing_face: :deaf_person:");
                return;
            }
            if (split.Length != 2)
            {
                await RespondAsync("Something went wrong with your command. :grimacing: Make sure you have both words and they are separated by a seimcolon ; with no spaces.");
                return;
            }

            if (split[0] == "")
            {
                await RespondAsync("Your first word is an empty string, we can't have that, silly!!");
                return;
            }
            if (split[1] == "")
            {
                await RespondAsync("Your second word is an empty string, keep yourself safe :hug: :knife:");
                return;
            }

            words.Add(split[0], split[1]);
            await RespondAsync($"Added word '{split[0]}' - '{split[1]}'");
        }

        [SlashCommand("showwords", "Output all added words so far.")]
        public async Task Showwords()
        {
            await RespondAsync("Those are the words I have in my memory: \n" + string.Join(Environment.NewLine, words));
        }

        public static string askWord = string.Empty;
        public static string answerWord = string.Empty;
        [SlashCommand("wordquestion", "print a word to translate to the opposite language.")]
        public async Task Wordquestion()
        {

            if (words.Count == 0)
            {
                await RespondAsync("There is no words to make questions from. Add some words first with /addword");
                return;
            }

            // .next zwraca INT 
            Random rng = new Random();
            int wordInd = rng.Next(0, words.Count); // min , max
            int wordSide = rng.Next(0, 1); // lewe czy prawe


            KeyValuePair<string, string> correctWords = words.ElementAt(wordInd);

            // 0 ? yes, left : no, right
            // 0 is key
            // 1 is value
            answerWord = (wordSide == 0 ? correctWords.Key : correctWords.Value);

            // and this here is flipped from this ^^^ so it asks the other word. So it asks for translaton of 1 and the correct answer is 0
            askWord = (wordSide == 1 ? correctWords.Key : correctWords.Value);

            await RespondAsync($"What is the translation of '{askWord}' ?");
            
        }

        [SlashCommand("wordanswer", "say a word to answer with.")]
        public async Task Wordanswer(string text)
        {
            if (answerWord == string.Empty)
            {
                await RespondAsync(":yellow_circle: :yellow_circle: There is no question being asked currently. :grimacing: :grimacing:  You can ask one with /wordquestion :yellow_circle: :yellow_circle: ");
                return;
            }

            if (text != answerWord)
            {
                await RespondAsync($":red_square::red_square: BZZZZT!!!! :red_square: INCORRECT!!! :smiling_imp: :smiling_imp:  The translation for '{askWord}' is NOT '{text}' !!!! >:((( :red_square: :red_square: :red_square:");
                return;
            }

            if (text == answerWord)
            {
                await RespondAsync($":green_square: :green_square: Correct! :green_square:  You are absolutely right! :partying_face: :partying_face: The translation for '{askWord}' is indeed '{answerWord}' !!!! :green_square: :green_square: ");
                
                // reset pytania
                askWord = string.Empty;
                answerWord = string.Empty;
                return;
            }

        }

    }
}
