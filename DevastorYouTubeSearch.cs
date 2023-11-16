
//__________________________________________________________________________________________________________________________________________________________
//__________________________________________________________________________________________________________________________________________________________
//__/\\\\\\\\\\\\_____/\\\\\\\\\\\\\\\__/\\\________/\\\_____/\\\\\\\\\________/\\\\\\\\\\\____/\\\\\\\\\\\\\\\_______/\\\\\_________/\\\\\\\\\_____________
//__\/\\\////////\\\__\/\\\///////////__\/\\\_______\/\\\___/\\\\\\\\\\\\\____/\\\/////////\\\_\///////\\\/////______/\\\///\\\_____/\\\///////\\\__________
//___\/\\\______\//\\\_\/\\\_____________\//\\\______/\\\___/\\\/////////\\\__\//\\\______\///________\/\\\_________/\\\/__\///\\\__\/\\\_____\/\\\_________
//____\/\\\_______\/\\\_\/\\\\\\\\\\\______\//\\\____/\\\___\/\\\_______\/\\\___\////\\\_______________\/\\\________/\\\______\//\\\_\/\\\\\\\\\\\/_________
//_____\/\\\_______\/\\\_\/\\\///////________\//\\\__/\\\____\/\\\\\\\\\\\\\\\______\////\\\____________\/\\\_______\/\\\_______\/\\\_\/\\\//////\\\________
//______\/\\\_______\/\\\_\/\\\________________\//\\\/\\\_____\/\\\/////////\\\_________\////\\\_________\/\\\_______\//\\\______/\\\__\/\\\____\//\\\______
//_______\/\\\_______ /\\\_\/\\\_________________\//\\\\\______\/\\\_______\/\\\__/\\\______\//\\\________\/\\\________\///\\\__/\\\____\/\\\_____\//\\\____
//________\/\\\\\\\\\\\\/ __\/\\\\\\\\\\\\\\\______\//\\\_______\/\\\_______\/\\\_\///\\\\\\\\\\\/_________\/\\\__________\///\\\\\/_____\/\\\______\//\\\__
//_________\////////////_____\///////////////________\///________\///________\///____\///////////___________\///_____________\/////_______\///________\///__
//__________________________________________________________________________________________________________________________________________________________
//__________________________________________________________________________________________________________________________________________________________
//_________________________________________________/\\\\\\\\\\\__/\\\\\_____/\\\________/\\\\\\\\\__________________________________________________________
//_________________________________________________\/////\\\///__\/\\\\\\___\/\\\_____/\\\////////__________________________________________________________
//______________________________________________________\/\\\_____\/\\\/\\\__\/\\\___/\\\/__________________________________________________________________
//_______________________________________________________\/\\\_____\/\\\//\\\_\/\\\__/\\\___________________________________________________________________
//________________________________________________________\/\\\_____\/\\\\//\\\\/\\\_\/\\\__________________________________________________________________
//_________________________________________________________\/\\\_____\/\\\_\//\\\/\\\_\//\\\________________________________________________________________
//__________________________________________________________\/\\\_____\/\\\__\//\\\\\\__\///\\\_____________________________________________________________
//________________________________________________________/\\\\\\\\\\\_\/\\\___\//\\\\\____\////\\\\\\\\\__/\\\_____________________________________________
//________________________________________________________\///////////__\///_____\/////________\/////////__\///_____________________________________________
//__________________________________________________________________________________________________________________________________________________________
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.Diagnostics;
using System.Linq;
using YoutubeExplode.Search;
using System.Threading;
using System.Text.Json.Serialization;
using YoutubeExplode.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace Google.Apis.YouTube.Samples
{
    internal class DevastorYouTubeSearch
    {
        private static string API_KEY_OPENAI = "";

        private List<string> DevastorFullVideoIdList = new List<string>();
        [STAThread]
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("SLAVA_UKRAINI", "1", EnvironmentVariableTarget.Process);
            string[] lines = File.ReadAllLines(@"../DATA.txt");
            API_KEY_OPENAI = lines[0];
            DevastorShowGreet();
            Console.WriteLine();
            Console.WriteLine("Введите поисковый запрос и нажмите Enter:");
            string video_search = Console.ReadLine();
            try
            {
                new DevastorYouTubeSearch().Run(video_search).Wait();
            }
            catch 
            { }                
            Console.WriteLine("Нажмите Enter для завершения программы...");
            Console.ReadKey();
		}

        private static void DevastorMessage(ConsoleColor foreground, ConsoleColor background, ConsoleColor shine)
        {
            char _dash = ' ';
            string _slash = "╗╚╝╔║═";
            char _backslash = '█';
            List<string> lines = new List<string>()
            {
                @"",
                @" ██████╗  ███████╗ ██╗   ██╗  █████╗  ███████╗ ████████╗  ██████╗  ██████╗ ",
                @" ██╔══██╗ ██╔════╝ ██║   ██║ ██╔══██╗ ██╔════╝ ╚══██╔══╝ ██╔═══██╗ ██╔══██╗",
                @" ██║  ██║ █████╗   ██║   ██║ ███████║ ███████╗    ██║    ██║   ██║ ██████╔╝",
                @" ██║  ██║ ██╔══╝   ╚██╗ ██╔╝ ██╔══██║ ╚════██║    ██║    ██║   ██║ ██╔══██╗",
                @" ██████╔╝ ███████╗  ╚████╔╝  ██║  ██║ ███████║    ██║    ╚██████╔╝ ██║  ██║",
                @" ╚═════╝  ╚══════╝   ╚═══╝   ╚═╝  ╚═╝ ╚══════╝    ╚═╝     ╚═════╝  ╚═╝  ╚═╝",
                @"",
                @"                         ██╗ ███╗   ██╗  ██████╗",
                @"                         ██║ ████╗  ██║ ██╔════╝",
                @"                         ██║ ██║╚██╗██║ ██║",
                @"                         ██║ ██║ ╚████║ ╚██████╗ ██╗",
                @"                         ╚═╝ ╚═╝  ╚═══╝  ╚═════╝ ╚═╝",
                @""
            };
            int cursor_top = Console.CursorTop;
            Console.SetCursorPosition(0, cursor_top);
            foreach (var line in lines)
            {
                foreach (var _char in line)
                { 
                    if (_char == _dash) Console.ForegroundColor = background;
                    if (_slash.Contains(_char.ToString())) Console.ForegroundColor = foreground;
                    if (_char == _backslash) Console.ForegroundColor = foreground;
                    Console.Write(_char);
                }
                Console.Write("\n");
                Thread.Sleep(50);
            }
            Console.CursorVisible = false;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(0, cursor_top);
                foreach (var line in lines)
                {
                    foreach (var _char in line)
                    {
                        if (_char == _dash) Console.ForegroundColor = background;
                        if (_slash.Contains(_char.ToString())) Console.ForegroundColor = foreground;
                        if (_char == _backslash) Console.ForegroundColor = foreground;
                        Console.Write(_char);
                    }
                    Console.Write("\n");
                }
                Thread.Sleep(200);
                Console.SetCursorPosition(0, cursor_top);
                foreach (var line in lines)
                {
                    foreach (var _char in line)
                    {
                        if (_char == _dash) Console.ForegroundColor = background;
                        if (_slash.Contains(_char.ToString())) Console.ForegroundColor = foreground;
                        if (_char == _backslash) Console.ForegroundColor = shine;
                        Console.Write(_char);
                    }
                    Console.Write("\n");
                }
                Thread.Sleep(200);
            }
            Console.WriteLine();
            cursor_top = Console.CursorTop;
            List<string> moving_lines = new List<string>()
            {
                @" █▄█ █▀█ █ █ ▀█▀ █ █ █▄▄ █▀▀   █▀▀ █ █ █▀█ █▀█ ▀█▀ █▀▀   █▀▀ █▀▀ █▄ █ █▀▀ █▀█ ▄▀█ ▀█▀ █▀█ █▀█",
                @"  █  █▄█ █▄█  █  █▄█ █▄█ ██▄   ▄▄█ █▀█ █▄█ █▀▄  █  ▄▄█   █▄█ ██▄ █ ▀█ ██▄ █▀▄ █▀█  █  █▄█ █▀▄"
            };
            int line_length = moving_lines[0].Length;
            for (int j = 0; j < line_length; j++)
            {
                Console.SetCursorPosition(0, cursor_top);
                foreach (var line in moving_lines)
                {
                    int char_num = 0;
                    foreach (var _char in line)
                    {
                        if (char_num <= j) Console.ForegroundColor = background;
                        else Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(_char);
                        char_num++;
                    }
                    Console.Write("\n");
                }
                //Thread.Sleep(1);
            }
            Console.CursorVisible = true;
            /*
            Console.WriteLine(Console.ForegroundColor);
            Console.WriteLine(@"__________________________________________________________________________________________________________________________________________________________");
            Console.WriteLine(@"__________________________________________________________________________________________________________________________________________________________");
            Console.WriteLine(@"__/\\\\\\\\\\\\_____/\\\\\\\\\\\\\\\__/\\\________/\\\_____/\\\\\\\\\________/\\\\\\\\\\\____/\\\\\\\\\\\\\\\_______/\\\\\_________/\\\\\\\\\_____________");
            Console.WriteLine(@"__\/\\\////////\\\__\/\\\///////////__\/\\\_______\/\\\___/\\\\\\\\\\\\\____/\\\/////////\\\_\///////\\\/////______/\\\///\\\_____/\\\///////\\\__________");
            Console.WriteLine(@"___\/\\\______\//\\\_\/\\\_____________\//\\\______/\\\___/\\\/////////\\\__\//\\\______\///________\/\\\_________/\\\/__\///\\\__\/\\\_____\/\\\_________");
            Console.WriteLine(@"____\/\\\_______\/\\\_\/\\\\\\\\\\\______\//\\\____/\\\___\/\\\_______\/\\\___\////\\\_______________\/\\\________/\\\______\//\\\_\/\\\\\\\\\\\/_________");
            Console.WriteLine(@"_____\/\\\_______\/\\\_\/\\\///////________\//\\\__/\\\____\/\\\\\\\\\\\\\\\______\////\\\____________\/\\\_______\/\\\_______\/\\\_\/\\\//////\\\________");
            Console.WriteLine(@"______\/\\\_______\/\\\_\/\\\________________\//\\\/\\\_____\/\\\/////////\\\_________\////\\\_________\/\\\_______\//\\\______/\\\__\/\\\____\//\\\______");
            Console.WriteLine(@"_______\/\\\_______ /\\\_\/\\\_________________\//\\\\\______\/\\\_______\/\\\__/\\\______\//\\\________\/\\\________\///\\\__/\\\____\/\\\_____\//\\\____");
            Console.WriteLine(@"________\/\\\\\\\\\\\\/ __\/\\\\\\\\\\\\\\\______\//\\\_______\/\\\_______\/\\\_\///\\\\\\\\\\\/_________\/\\\__________\///\\\\\/_____\/\\\______\//\\\__");
            Console.WriteLine(@"_________\////////////_____\///////////////________\///________\///________\///____\///////////___________\///_____________\/////_______\///________\///__");
            Console.WriteLine(@"__________________________________________________________________________________________________________________________________________________________");
            Console.WriteLine(@"__________________________________________________________________________________________________________________________________________________________");  
            Console.WriteLine(@"_________________________________________________/\\\\\\\\\\\__/\\\\\_____/\\\________/\\\\\\\\\__________________________________________________________");
            Console.WriteLine(@"_________________________________________________\/////\\\///__\/\\\\\\___\/\\\_____/\\\////////__________________________________________________________");
            Console.WriteLine(@"______________________________________________________\/\\\_____\/\\\/\\\__\/\\\___/\\\/__________________________________________________________________");
            Console.WriteLine(@"_______________________________________________________\/\\\_____\/\\\//\\\_\/\\\__/\\\___________________________________________________________________");
            Console.WriteLine(@"________________________________________________________\/\\\_____\/\\\\//\\\\/\\\_\/\\\__________________________________________________________________");
            Console.WriteLine(@"_________________________________________________________\/\\\_____\/\\\_\//\\\/\\\_\//\\\________________________________________________________________");
            Console.WriteLine(@"__________________________________________________________\/\\\_____\/\\\__\//\\\\\\__\///\\\_____________________________________________________________");
            Console.WriteLine(@"________________________________________________________/\\\\\\\\\\\_\/\\\___\//\\\\\____\////\\\\\\\\\__/\\\_____________________________________________");
            Console.WriteLine(@"________________________________________________________\///////////__\///_____\/////________\/////////__\///_____________________________________________");
            Console.WriteLine(@"__________________________________________________________________________________________________________________________________________________________");
             */
        }

        private static void DevastorShowGreet()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            DevastorMessage(ConsoleColor.DarkCyan, ConsoleColor.DarkGray, ConsoleColor.Cyan);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        private async Task<string> DevastorSummarizeText(string input_text)
        {
            string summary_text = "";
            try
            {
                summary_text = await DevastorCallPythonAnalyzer(input_text);// "Summarize input russian text and give answer in russian: " + input_text);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR summarize: " + e.Message);
            }
            return summary_text;
        }

        public async Task<string> DevastorCallPythonAnalyzer(string BIG_TEXT)
        {
            return await Task.Run(() =>
            {
                string pythonScript = "DevastorAnalyze.py";
                string escapedText = BIG_TEXT.Replace("\"", "\\\""); // Экранируем двойные кавычки

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "python3",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"{pythonScript} \"{escapedText}\"" // Передаем экранированный текст
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine("Error: " + error);
                        return null;
                    }
                    string pattern = @"New pypi version:.+";
                    output = Regex.Replace(output, pattern, "");
                    return output;
                }
            });
            /*
            string responseText = "";
            string apiKey = API_KEY_OPENAI;
            string endpoint = "https://api.openai.com/v1/chat/completions";

            // формируем отправляемые данные и сериализуем их в JSON
            var messages = new[]
            {
                new { role = "user", content = BIG_TEXT }
            };
            var data = new
            {
                model = "gpt-3.5-turbo",
                messages = messages,
                temperature = 0.7
            };
            string jsonString = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            try
            {

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var response = await httpClient.PostAsync(endpoint, content);
                string responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"responseContent: {responseContent}");
                var jsonResponse = JObject.Parse(responseContent);
                var assistantMessageContent = jsonResponse["choices"][0]["message"]["content"].Value<string>();
                responseText = assistantMessageContent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null; // или бросьте исключение, в зависимости от вашей логики обработки ошибок
            }
            return responseText;*/
        }

        private async Task DevastorYouTubeSearcher(string search_text)
        {
            /*
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = API_KEY_YOUTUBE,
            });

            // Выполните запрос к YouTube API для получения популярных запросов
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Chart = SearchResource.ListRequest.ChartEnum.MostPopular;
            searchListRequest.MaxResults = 10;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            Console.WriteLine("Топ 10 популярных запросов на YouTube:");

            foreach (var searchResult in searchListResponse.Items)
            {
                Console.WriteLine(searchResult.Snippet.Title);
            }
            */
            var youtube = new YoutubeClient();
            Console.WriteLine("Приступаю к поиску...");
            var search_list = await youtube.Search.GetResultsAsync(search_text);
            var count = search_list.Count;
            Console.WriteLine("Найдено " + count + " элементов! Приступаю к анализу...");
            foreach (var result in search_list)
            {
                switch (result)
                {
                    case VideoSearchResult video:
                        {
                            var id = video.Id;
                            var title = video.Title;
                            var duration = video.Duration;
                            string VIDEO_ID = video.Id;
                            string VIDEO_NAME = video.Title;
                            Console.WriteLine("Видео " + DevastorFullVideoIdList.Count + ": " + VIDEO_NAME + " ( " + duration + " секунд )");
                            DevastorFullVideoIdList.Add(VIDEO_ID);
                            // Транскрипция видео (YouTubeExplode)
                            if (duration.Value.Seconds > 5 && duration.Value.Seconds < 60)
                            {
                                string DevastorTranscribeString = "";
                                try
                                {
                                    int main_subjects_start_sec = 0;
                                    int main_subjects_end_sec = 0;
                                    Console.WriteLine("Создание транскрипции...");
                                    var trackManifest = await youtube.Videos.ClosedCaptions.GetManifestAsync("https://www.youtube.com/watch?v=" + VIDEO_ID);
                                    var trackInfo = trackManifest.GetByLanguage("ru");
                                    var track = await youtube.Videos.ClosedCaptions.GetAsync(trackInfo);
                                    string last_phrase = "";
                                    Console.WriteLine("Транскрипция видео получена!");
                                    Console.WriteLine("Поиск наиболее просматриваемого эпизода...");
                                    HttpClient req = new HttpClient();
                                    var content = await req.GetAsync("https://yt.lemnoslife.com/videos?part=mostReplayed&id=" + VIDEO_ID);
                                    var jsonString = await content.Content.ReadAsStringAsync();                                    
                                    DevastorYTModel json_object = JsonConvert.DeserializeObject<DevastorYTModel>(jsonString);
                                    // Получаем все маркеры
                                    var markers = json_object.items[0].mostReplayed.markers;
                                    // Определяем допустимый диапазон времени (+5 секунд с начала и -5 секунд до конца видео)
                                    long startTimeLimit = 5000; // 5 секунд
                                    long endTimeLimit = markers[markers.Count - 1].startMillis;
                                    // Фильтруем маркеры, оставляя только те, которые находятся в пределах допустимого диапазона
                                    var validMarkers = markers.Where(marker => marker.startMillis >= startTimeLimit && marker.startMillis <= endTimeLimit).ToList();
                                    // Если есть хотя бы один допустимый маркер
                                    if (validMarkers.Any())
                                    {
                                        // Находим максимальное значение startMillis среди допустимых маркеров
                                        long maxStartMillis = validMarkers.Max(marker => marker.startMillis);
                                        // Ищем левую и правую границы интервала
                                        long leftBoundary = maxStartMillis;
                                        long rightBoundary = maxStartMillis;
                                        // Идем влево от максимального маркера, пока startMillis убывают
                                        int leftIndex = validMarkers.FindIndex(marker => marker.startMillis == maxStartMillis);
                                        while (leftIndex > 0 && validMarkers[leftIndex - 1].startMillis < validMarkers[leftIndex].startMillis)
                                        {
                                            leftIndex--;
                                            leftBoundary = validMarkers[leftIndex].startMillis;
                                        }
                                        // Идем вправо от максимального маркера, пока startMillis убывают
                                        int rightIndex = validMarkers.FindIndex(marker => marker.startMillis == maxStartMillis);
                                        while (rightIndex < validMarkers.Count - 1 && validMarkers[rightIndex + 1].startMillis > validMarkers[rightIndex].startMillis)
                                        {
                                            rightIndex++;
                                            rightBoundary = validMarkers[rightIndex].startMillis;
                                        }
                                        // Теперь у нас есть левая (leftBoundary) и правая (rightBoundary) границы интервала
                                        main_subjects_start_sec = Convert.ToInt32(leftBoundary / 1000);
                                        main_subjects_end_sec = Convert.ToInt32(rightBoundary / 1000);


                                        Console.WriteLine("Наиболее просматриваеый эпизод найден!");
                                        Console.WriteLine("Выделение транскрипции эпизода...");
                                        for (int _t = main_subjects_start_sec; _t < main_subjects_end_sec; _t++)
                                        {
                                            //Console.WriteLine("Поиск фразы...");
                                            try
                                            {
                                                var caption = track.GetByTime(TimeSpan.FromSeconds(_t));
                                                var current_phrase = caption.Text;
                                                if (current_phrase != last_phrase)
                                                {
                                                    DevastorTranscribeString += current_phrase + " ";
                                                    last_phrase = current_phrase;
                                                    //Console.WriteLine("Фраза " + _t + " добавлена в массив транскрипции! ");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                //Console.WriteLine("ERROR GetByTime: " + e.Message);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("В пределах допустимого диапазона нет маркеров.");
                                    }

                                }
                                catch { }
                                Console.WriteLine("Транскрипция эпизода получена!");
                                if (DevastorTranscribeString != "")
                                {
                                    Console.WriteLine("Выделение главных компонент транскрипции...");
                                    string summary = await DevastorSummarizeText(DevastorTranscribeString);
                                    Console.WriteLine("Главные компоненты транскрипции получены!");
                                    Console.WriteLine("Изложение: " + summary);
                                    //Console.WriteLine();
                                    //Console.WriteLine("Нажмите Enter для перехода к следующему ролику --->");
                                    //Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Транскрипция видео не содержит элементов!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Видео не подходит по продолжительности! ( требование: 5-60 сек. )");
                            }
                            break;
                        }
                    case PlaylistSearchResult playlist:
                        {
                            var id = playlist.Id;
                            var title = playlist.Title;
                            break;
                        }
                    case ChannelSearchResult channel:
                        {
                            var id = channel.Id;
                            var title = channel.Title;
                            break;
                        }
                }

            }
        }

        private async Task Run(string video_search)
        {
            await DevastorYouTubeSearcher(video_search);
        }
        private double GetIntensityScore(DevastorTimedMarkerDecoration timedMarkerDecoration, List<DevastorMarker> markers)
        {
            var marker = markers.FirstOrDefault(m => m.startMillis == timedMarkerDecoration.visibleTimeRangeStartMillis);
            if (marker != null)
            {
                return marker.intensityScoreNormalized;
            }
            return 0.0;
        }
        private string CleanFileName(string fileName)
        {
            string invalidChars = new string(Path.GetInvalidFileNameChars());
            string cleanedFileName = new string(fileName.Where(x => !invalidChars.Contains(x)).ToArray());
            cleanedFileName = cleanedFileName.Replace("\"", "*");
            return cleanedFileName;
        }
        private async Task DownloadVideoSegment(string search_text, string video_name, string videoId, DevastorTimedMarkerDecoration timedMarkerDecoration=null, List<long> time_list=null)
        {
            int FRAME_SIZE = 100;
            var youtube = new YoutubeClient();
            video_name = CleanFileName(video_name);
            var videoUrl = "https://youtube.com/watch?v=" + videoId;
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();
            var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
            string outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "Videos", search_text);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            double bitrate = streamInfo.Bitrate.KiloBitsPerSecond;
            var ffmpeg = @"/usr/local/bin/ffmpeg";
            long start_time = 0;
            long end_time = 0;
            if (timedMarkerDecoration != null)
            {
                start_time = timedMarkerDecoration.visibleTimeRangeStartMillis - FRAME_SIZE;
                end_time = timedMarkerDecoration.visibleTimeRangeEndMillis + FRAME_SIZE;
            }
            else if (time_list != null)
            {
                start_time = time_list[0] - FRAME_SIZE;
                end_time = time_list[1];// + FRAME_SIZE;
            }
            TimeSpan startTimeSpan = TimeSpan.FromMilliseconds(start_time);
            TimeSpan endTimeSpan = TimeSpan.FromMilliseconds(end_time);
            var durationSpan = endTimeSpan - startTimeSpan;
            if (durationSpan.TotalSeconds >= 5 && durationSpan.TotalSeconds <= 30 && startTimeSpan.TotalSeconds > 0)
            {   // Видео продолжительностью от 5 до 30 секунд и это не самое начало видео
                var startFormatted = $"{startTimeSpan.Hours:00}:{startTimeSpan.Minutes:00}:{startTimeSpan.Seconds:00}.{startTimeSpan.Milliseconds:000}";
                var endFormatted = $"{endTimeSpan.Hours:00}:{endTimeSpan.Minutes:00}:{endTimeSpan.Seconds:00}.{endTimeSpan.Milliseconds:000}";
                var durationFormatted = $"{durationSpan.Hours:00}:{durationSpan.Minutes:00}:{durationSpan.Seconds:00}.{durationSpan.Milliseconds:000}";
                var arguments = $"-loglevel error -ss {startFormatted} -i \"{streamInfo.Url}\" -t {durationFormatted} -c copy \"../Videos/{search_text}/{video_name}_({videoId})_from_{startFormatted}_to_{endFormatted}_<{bitrate:0}kbps>.mp4\"";
                using (Process p = new Process())
                {
                    p.StartInfo.FileName = ffmpeg;
                    p.StartInfo.Arguments = arguments;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.Start();
                    p.WaitForExit();
                    var output = p.StandardOutput.ReadToEnd();
                }
                Console.WriteLine(video_name + " : " + durationSpan.TotalSeconds + " сек.  (ссылка: https://www.youtube.com/watch?v=" + videoId + " )");
            }
            else
            {
                //Console.WriteLine(" отрывок не подходит:");
            }
        }
    }
    class DevastorYTModel
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public List<DevastorYTModelItem> items { get; set; }
    }
    class DevastorYTModelItem
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public DevastorMostReplayed mostReplayed { get; set; }
    }
    class DevastorMostReplayed
    {
        public List<DevastorMarker> markers { get; set; }
        public List<DevastorTimedMarkerDecoration> timedMarkerDecorations { get; set; }
    }
    class DevastorMarker
    {
        public long startMillis { get; set; }
        public double intensityScoreNormalized { get; set; }
    }
    class DevastorTimedMarkerDecoration
    {
        public long visibleTimeRangeStartMillis { get; set; }
        public long visibleTimeRangeEndMillis { get; set; }
    }
}


/*


                            // Транскрипция видео (youtube-transcript-api)
                            /*
                            using (var youTubeTranscriptApi = new DevastorYoutubeTranscriptApi.DevastorYoutubeTranscriptApi())
                            {
                                Console.WriteLine(VIDEO_NAME + "( ссылка: https://www.youtube.com/watch?v=" + VIDEO_ID + " ) ТРАНСКРИПЦИЯ:");
                                try
                                {
                                    var DevastorTranscriptedItems = youTubeTranscriptApi.GetTranscript(VIDEO_ID, new[] { "ru" });
                                    foreach (DevastorYoutubeTranscriptApi.TranscriptItem prhaseContainer in DevastorTranscriptedItems)
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Начало:" + prhaseContainer.Start);
                                        Console.WriteLine("Длительность:" + prhaseContainer.Duration);
                                        Console.WriteLine("Фраза:" + prhaseContainer.Text);
                                        Console.WriteLine();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error: " + ex.Message);
                                }                                
                            }*/
// Выделение главных компонент
/*
HttpClient req = new HttpClient();
var content = await req.GetAsync("https://yt.lemnoslife.com/videos?part=mostReplayed&id=" + VIDEO_ID);
var jsonString = await content.Content.ReadAsStringAsync();
try
{
    DevastorYTModel json_object = JsonConvert.DeserializeObject<DevastorYTModel>(jsonString);

    // Получаем все маркеры
    var markers = json_object.items[0].mostReplayed.markers;

    // Определяем допустимый диапазон времени (+5 секунд с начала и -5 секунд до конца видео)
    long startTimeLimit = 5000; // 5 секунд
    long endTimeLimit = markers[markers.Count - 1].startMillis;

    // Фильтруем маркеры, оставляя только те, которые находятся в пределах допустимого диапазона
    var validMarkers = markers.Where(marker => marker.startMillis >= startTimeLimit && marker.startMillis <= endTimeLimit).ToList();

    // Если есть хотя бы один допустимый маркер
    if (validMarkers.Any())
    {
        // Находим максимальное значение startMillis среди допустимых маркеров
        long maxStartMillis = validMarkers.Max(marker => marker.startMillis);

        // Ищем левую и правую границы интервала
        long leftBoundary = maxStartMillis;
        long rightBoundary = maxStartMillis;

        // Идем влево от максимального маркера, пока startMillis убывают
        int leftIndex = validMarkers.FindIndex(marker => marker.startMillis == maxStartMillis);
        while (leftIndex > 0 && validMarkers[leftIndex - 1].startMillis < validMarkers[leftIndex].startMillis)
        {
            leftIndex--;
            leftBoundary = validMarkers[leftIndex].startMillis;
        }

        // Идем вправо от максимального маркера, пока startMillis убывают
        int rightIndex = validMarkers.FindIndex(marker => marker.startMillis == maxStartMillis);
        while (rightIndex < validMarkers.Count - 1 && validMarkers[rightIndex + 1].startMillis > validMarkers[rightIndex].startMillis)
        {
            rightIndex++;
            rightBoundary = validMarkers[rightIndex].startMillis;
        }

        // Теперь у вас есть левая (leftBoundary) и правая (rightBoundary) границы интервала
        //Console.WriteLine("Левая граница интервала: " + leftBoundary);
        //Console.WriteLine("Правая граница интервала: " + rightBoundary);
        await DownloadVideoSegment(search_text, VIDEO_NAME, VIDEO_ID, null, new List<long>() { leftBoundary, rightBoundary });
    }
    else
    {
        // В допустимом диапазоне нет маркеров
        Console.WriteLine("В пределах допустимого диапазона нет маркеров.");
    }


    /*
    var timedMarkerDecorationWithHighestIntensity = json_object.items[0].mostReplayed.timedMarkerDecorations
        .OrderByDescending(d => GetIntensityScore(d, json_object.items[0].mostReplayed.markers))
        .FirstOrDefault();

    if (timedMarkerDecorationWithHighestIntensity != null)
    {
        // Получаем значение visibleTimeRangeStartMillis из timedMarkerDecorationWithHighestIntensity
        long targetStartMillis = timedMarkerDecorationWithHighestIntensity.visibleTimeRangeStartMillis;

        // Находим все маркеры для текущего элемента
        var markers = json_object.items[0].mostReplayed.markers;

        // Ищем индекс маркера с соответствующим startMillis
        int targetMarkerIndex = markers.FindIndex(marker => marker.startMillis == targetStartMillis);

        if (targetMarkerIndex >= 0)
        {
            // Получаем контейнер совпавшего маркера
            DevastorMarker targetMarker = markers[targetMarkerIndex];

            // Получаем контейнеры перед и после маркера (если они существуют)
            DevastorMarker previousMarker = targetMarkerIndex > 0 ? markers[targetMarkerIndex - 1] : null;
            DevastorMarker nextMarker = targetMarkerIndex < markers.Count - 1 ? markers[targetMarkerIndex + 1] : null;

            // Теперь у вас есть targetMarker, previousMarker и nextMarker (если они существуют)
            // Вы можете использовать их в дальнейшем коде

            Console.WriteLine();
            Console.WriteLine("Начало:" + previousMarker.startMillis);
            Console.WriteLine("Плотность:" + previousMarker.intensityScoreNormalized);
            Console.WriteLine();
            Console.WriteLine("Начало:" + targetMarker.startMillis);
            Console.WriteLine("Плотность:" + targetMarker.intensityScoreNormalized);
            Console.WriteLine();
            Console.WriteLine("Начало:" + nextMarker.startMillis);
            Console.WriteLine("Плотность:" + nextMarker.intensityScoreNormalized);
            Console.WriteLine();
        }
        else
        {
            // Маркер с указанным startMillis не найден
            // Обработайте эту ситуацию по вашему усмотрению
        }

        //await DownloadVideoSegment(search_text, VIDEO_NAME, VIDEO_ID, timedMarkerDecorationWithHighestIntensity);
    }
}
catch (Exception ex)
{
    //Console.WriteLine("Error: " + ex.Message);
}*/



/*

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Reflection;
using System.Text;
using System.Threading;


private string API_KEY = "";

string[] lines = File.ReadAllLines(@"../DATA.txt");
API_KEY = lines[0];
var youtubeService = new YouTubeService(new BaseClientService.Initializer()
{
    ApiKey = API_KEY,
    ApplicationName = this.GetType().ToString()
});

var searchListRequest = youtubeService.Search.List("snippet");
searchListRequest.Q = "Мотивационный ролик";
searchListRequest.MaxResults = 100;
var searchListResponse = await searchListRequest.ExecuteAsync();
List<string> videos = new List<string>();
List<string> channels = new List<string>();
List<string> playlists = new List<string>();
foreach (var searchResult in searchListResponse.Items)
{
    switch (searchResult.Id.Kind)
    {
        case "youtube#video":
            string VIDEO_ID = searchResult.Id.VideoId;
            string VIDEO_NAME = searchResult.Snippet.Title;
            DevastorFullVideoIdList.Add(VIDEO_ID);

            // Get Request for video metadata
            HttpClient req = new HttpClient();
            var content = await req.GetAsync("https://yt.lemnoslife.com/videos?part=mostReplayed&id=" + VIDEO_ID);
            var jsonString = await content.Content.ReadAsStringAsync();

            try
            {
                DevastorYTModel json_object = JsonConvert.DeserializeObject<DevastorYTModel>(jsonString);

                // Find the TimedMarkerDecoration with the highest intensityScoreNormalized
                var timedMarkerDecorationWithHighestIntensity = json_object.items[0].mostReplayed.timedMarkerDecorations
                    .OrderByDescending(d => GetIntensityScore(d, json_object.items[0].mostReplayed.markers))
                    .FirstOrDefault();

                if (timedMarkerDecorationWithHighestIntensity != null)
                {
                    Console.WriteLine(VIDEO_NAME + " : " + timedMarkerDecorationWithHighestIntensity.visibleTimeRangeStartMillis);

                    // Download the video segment
                    await DownloadVideoSegment(VIDEO_NAME, VIDEO_ID, timedMarkerDecorationWithHighestIntensity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            break;
        default:
            break;
    }
}
*/