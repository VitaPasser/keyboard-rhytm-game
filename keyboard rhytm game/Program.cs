using SFML.Audio;
using System.Threading.Tasks.Sources;
using static SFML.Window.Keyboard;

namespace keyboard_rhytm_game
{
    internal class Program
    {

        class GlobalVariable
        {
            public static Task variableTask;
            public static char? litteral = null;
            public static int score;
            public static int turnNow;

            public static async Task asa()
            {
                await variableTask;
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("keyboard rhytm game 0.1.1");
            Music music = new("music1.wav");

            Object[][] keyDoNeedPress = new Object[8][];
            char literal = 'A';
            for (int i = 0; i < 8; i++)
            {
                keyDoNeedPress[i] = new Object[3];
                keyDoNeedPress[i][0] = literal++;
                if (i == 0)
                {
                    keyDoNeedPress[i][1] = 4;
                }
                else
                {
                    keyDoNeedPress[i][1] = 1;
                }
                keyDoNeedPress[i][2] = 1;
            }

            //music.Loop = true;


            Func<Task> func1 = async () =>
            {
                do
                {
                    if (music.Status != SoundStatus.Paused)
                        //Console.WriteLine(music.PlayingOffset.AsMilliseconds());
                        await Task.Delay(1);

                } while (music.Status != SoundStatus.Stopped);
            };



            async Task func3(Object[] key, int index)
            {
                int timeToEnd = (int)key[2] * 1000,
                    start = music.PlayingOffset.AsMilliseconds(),
                    endNext = start + timeToEnd;
                do
                {
                    if (GlobalVariable.litteral != null && index == GlobalVariable.turnNow)
                    {
                        Console.WriteLine($"\tBad\n");
                        GlobalVariable.litteral = null;
                        GlobalVariable.turnNow++;
                        return;
                    }

                    await Task.Delay(start + timeToEnd/3 - music.PlayingOffset.AsMilliseconds());
                    if (GlobalVariable.litteral != null && GlobalVariable.litteral == (char)key[0])
                    {
                        GlobalVariable.score += 100;
                        Console.WriteLine($"\t100+\n");
                        GlobalVariable.litteral = null;
                        GlobalVariable.turnNow++;
                        return;
                    }
                    else if (GlobalVariable.litteral != null && GlobalVariable.litteral != (char)key[0])
                    {
                        Console.WriteLine($"\tBad\n");
                        GlobalVariable.litteral = null;
                        GlobalVariable.turnNow++;
                        return;
                    }
                    await Task.Delay(start + (2 * timeToEnd) / 3 - music.PlayingOffset.AsMilliseconds());
                    if (GlobalVariable.litteral != null && GlobalVariable.litteral == (char)key[0])
                    {
                        GlobalVariable.score += 200;
                        Console.WriteLine($"\t200+\n");
                        GlobalVariable.litteral = null;
                        GlobalVariable.turnNow++;
                        return;
                    }
                    else if (GlobalVariable.litteral != null && GlobalVariable.litteral != (char)key[0])
                    {
                        Console.WriteLine($"\tBad\n");
                        GlobalVariable.litteral = null;
                        GlobalVariable.turnNow++;
                        return;
                    }
                    await Task.Delay(start + timeToEnd - music.PlayingOffset.AsMilliseconds());
                    if (GlobalVariable.litteral != null && GlobalVariable.litteral == (char)key[0])
                    {
                        GlobalVariable.score += 300;
                        Console.WriteLine($"\t300+\n");
                        GlobalVariable.litteral = null;
                        GlobalVariable.turnNow++;
                        return;
                    }
                    else if (GlobalVariable.litteral != null && GlobalVariable.litteral != (char)key[0])
                    {
                        Console.WriteLine($"\tBad\n");
                        GlobalVariable.litteral = null;
                        GlobalVariable.turnNow++;
                        return;
                    }
                } while (music.PlayingOffset.AsMilliseconds() < endNext);
                GlobalVariable.litteral = null;
                Console.WriteLine($"\t{key[0]} - Not");
                GlobalVariable.turnNow++;
            }

            /*
            Func<Task> func3 = async () =>
            {
                foreach (var key in keyDoNeedPress)
                {
                    int timeToNext = (int)key[1] * 1000,
                        timeToEnd = (int)key[2] * 1000,
                        start = music.PlayingOffset.AsMilliseconds(),
                        endNext = start + timeToEnd;
                    do
                    {
                        await Task.Delay(endNext - music.PlayingOffset.AsMilliseconds());
                    } while (music.PlayingOffset.AsMilliseconds() < endNext);
                    Console.WriteLine($"{key[0]} - Not");
                }

            };
            */

            Func<Task> func2 = async () =>
            {
                int index = 0;
                GlobalVariable.turnNow = index;
                for (; index < keyDoNeedPress.Length; index++)
                {

                    int timeToNext = (int)keyDoNeedPress[index][1] * 1000,
                        start = music.PlayingOffset.AsMilliseconds(),
                        endNext = start + timeToNext;
                    do {
                        await Task.Delay(endNext - music.PlayingOffset.AsMilliseconds());
                    } while (music.PlayingOffset.AsMilliseconds() < endNext);
                    Console.WriteLine(keyDoNeedPress[index][0]);
                    GlobalVariable.variableTask = func3(keyDoNeedPress[index], index);
                }

            };


            music.Play();
            //var func11 = func1();
            var func22 = func2();


            do
            {
                int key = 0;
                key = (int)Console.ReadKey(true).Key; 
                if (key >= 65 && key <= 90)
                {
                    Console.Write((char)(key + 32));
                    GlobalVariable.litteral = (char)key;
                }

                if (key == 32 && SoundStatus.Paused == music.Status)
                {
                    music.Play();
                }
                else if (key == 32 && SoundStatus.Playing == music.Status)
                {
                    music.Pause();
                }

            } while (music.Status != SoundStatus.Stopped);

            /*
            await GlobalVariable.asa();
            await func22;
            */
            await Task.WhenAny(GlobalVariable.asa(), func22);
            //await func11;

            Console.WriteLine($"Score: {GlobalVariable.score}");
            Console.WriteLine($"{GlobalVariable.score / 2400M * 100}%" );
            Console.Read();
        }
    }
}