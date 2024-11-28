using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace dz5
{
    class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int YearOfBirth { get; set; }
        public string Exam { get; set; }
        public int Score { get; set; }
    }

    class Grandma
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Diseases { get; set; } = new List<string>();
        public List<string> Medicines { get; set; } = new List<string>();

        public Grandma(string name, int age, List<string> diseases, List<string> medicines)
        {
            Name = name;
            Age = age;
            Diseases = diseases;
            Medicines = medicines;
        }
    }

    class Hospital
    {
        public string Name { get; set; }
        public List<string> TreatedDiseases { get; set; } = new List<string>();
        public int Capacity { get; set; }
        public Queue<Grandma> Grandmas { get; set; } = new Queue<Grandma>();

        public Hospital(string name, List<string> treatedDiseases, int capacity)
        {
            Name = name;
            TreatedDiseases = treatedDiseases;
            Capacity = capacity;
        }

        public double OccupancyPercentage => (double)Grandmas.Count / Capacity * 100;
    }
    class Failik

    {
        static List<Student> students = new List<Student>();

        static async Task<List<byte[]>> DownloadImages(List<string> urls)
        {
            List<byte[]> images = new List<byte[]>();
            using (HttpClient client = new HttpClient())
            {
                foreach (var url in urls)
                {
                    var imageBytes = await client.GetByteArrayAsync(url);
                    images.Add(imageBytes);
                }
            }
            return images;
        }

        static void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }


        static void RunGrandmaHospitalProgram()
        {
            List<Grandma> grandmas = new List<Grandma>();
            Stack<Hospital> hospitals = new Stack<Hospital>();
            hospitals.Push(new Hospital("Больница 1", new List<string> { "грипп", "кашель" }, 2));
            hospitals.Push(new Hospital("Больница 2", new List<string> { "простуда", "боли в спине" }, 2));
            hospitals.Push(new Hospital("Больница 3", new List<string> { "грипп", "боли в спине" }, 1));

            Console.Write("Введите количество бабушек для ввода: ");
            int numberOfGrandmas = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfGrandmas; i++)
            {
                Console.WriteLine($"\nВведите данные для бабушки {i + 1}:");
                Console.Write("Введите имя бабушки: ");
                string name = Console.ReadLine();

                Console.Write("Введите возраст бабушки: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Введите болезни бабушки (через запятую): ");
                List<string> diseases = Console.ReadLine().Split(',').Select(d => d.Trim()).ToList();

                Console.Write("Введите лекарства от болезней (через запятую): ");
                List<string> medicines = Console.ReadLine().Split(',').Select(m => m.Trim()).ToList();

                Grandma grandma = new Grandma(name, age, diseases, medicines);
                grandmas.Add(grandma);

                AssignGrandmaToHospital(grandma, hospitals);
            }

            DisplayGrandmas(grandmas, hospitals);
        }

        static void AssignGrandmaToHospital(Grandma grandma, Stack<Hospital> hospitals)
        {
            if (grandma.Diseases.Count == 0)
            {
                foreach (var hospital in hospitals)
                {
                    if (hospital.Grandmas.Count < hospital.Capacity)
                    {
                        hospital.Grandmas.Enqueue(grandma);
                        Console.WriteLine($"{grandma.Name} попала в больницу '{hospital.Name}' (без болезней)");
                        return;
                    }
                }
                Console.WriteLine($"{grandma.Name} не нашла свободную больницу и осталась на улице плакать.");
                return;
            }

            foreach (var hospital in hospitals)
            {

                int treatedDiseasesCount = hospital.TreatedDiseases.Intersect(grandma.Diseases).Count();
                if (treatedDiseasesCount / (double)grandma.Diseases.Count > 0.5 && hospital.Grandmas.Count < hospital.Capacity)
                {
                    hospital.Grandmas.Enqueue(grandma);
                    Console.WriteLine($"{grandma.Name} попала в больницу '{hospital.Name}'");
                    return;
                }
            }

            Console.WriteLine($"{grandma.Name} не нашла больницу, которая лечит ее болезни и осталась на улице плакать.");
        }

        static void DisplayGrandmas(List<Grandma> grandmas, Stack<Hospital> hospitals)
        {
            Console.WriteLine("\nСписок бабушек:");
            foreach (var grandma in grandmas)
            {
                Console.WriteLine($"Имя: {grandma.Name}, Возраст: {grandma.Age}, Болезни: {string.Join(", ", grandma.Diseases)}");
            }

            Console.WriteLine("\nСписок больниц:");
            foreach (var hospital in hospitals)
            {
                Console.WriteLine($"Название: {hospital.Name}, Болезни: {string.Join(", ", hospital.TreatedDiseases)}, Заполненность: {hospital.OccupancyPercentage:F2}%");
                Console.WriteLine($"Количество бабушек в больнице: {hospital.Grandmas.Count}");
            }
        }
        static async Task Main(string[] args)
        {
            Console.WriteLine("Задание 1");
            List<string> imageUrls = new List<string>
        {
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fcont.ws%2Fuploads%2Fpic%2F2019%2F11%2F2017Nature___Volcanoes_Lightning_above_the_black_smoke_erupting_volcano_Colima_113560_.jpg&lr=172&nl=1&pos=1&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=http%3A%2F%2Fimg-s-msn-com.akamaized.net%2Ftenant%2Famp%2Fentityid%2FAAOKoon.img&lr=172&nl=1&pos=2&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fhdpic.club%2Fuploads%2Fposts%2F2022-01%2F1643092240_30-hdpic-club-p-vulkan-samolet-foto-54.jpg&lr=172&nl=1&pos=3&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fzendiar.com%2Fwp-content%2Fuploads%2F2023%2F09%2Fesli-v-nedrah-zemli-idet-raspad-radioaktivnyh-elementov-to-pochemu-lava-ne-radioaktivna-666ea88.jpg&lr=172&nl=1&pos=4&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=http%3A%2F%2Fcdnstatic.rg.ru%2Fuploads%2Fimages%2F167%2F74%2F64%2FiStock-451543103.jpg&lr=172&nl=1&pos=5&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fpic.rutubelist.ru%2Fvideo%2Fb0%2F6b%2Fb06b9a83fae7947703216035ccf6ae0e.jpg&lr=172&nl=1&pos=6&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fpic.rutubelist.ru%2Fvideo%2Fb0%2F6b%2Fb06b9a83fae7947703216035ccf6ae0e.jpg&lr=172&nl=1&pos=6&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fsneg.top%2Fuploads%2Fposts%2F2023-04%2F1681932818_sneg-top-p-izverzhenie-vulkana-kartinki-vkontakte-6.jpg&lr=172&nl=1&pos=8&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fpeakfinder.ru%2Fimage%2Foriginal%2F11_apo.jpg&lr=172&nl=1&pos=9&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fimg.s-msn.com%2Ftenant%2Famp%2Fentityid%2FBB9vLiw.img&lr=172&nl=1&pos=10&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fops.group%2Fblog%2Fwp-content%2Fuploads%2F2016%2F09%2FIceland-Katla.png&lr=172&nl=1&pos=11&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Favatars.dzeninfra.ru%2Fget-zen-vh%2F9832375%2F2a000001896dfdbc67ae380cc1ecf0a7787a%2Forig&lr=172&nl=1&pos=12&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Ftayniymir.com%2Fwp-content%2Fuploads%2F2017%2F06%2F96i594ac30b8bd798.23135021.jpg&lr=172&nl=1&pos=13&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
             "https://yandex.ru/images/search?img_url=https%3A%2F%2Fdessindigo.com%2Fstorage%2Fimages%2Fposts%2Fvolcan%2Fphoto-reference-volcan-2.jpg&lr=172&nl=1&pos=14&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fwww.funnyart.club%2Fuploads%2Fposts%2F2023-05%2F1682899412_funnyart-club-p-izverzhenie-vulkana-foto-23.jpg&lr=172&nl=1&pos=15&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fimg-fotki.yandex.ru%2Fget%2F122076%2F137106206.6bf%2F0_1d54d5_7fd04cd7_orig.jpg&lr=172&nl=1&pos=16&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fpanorama.pub%2Fstorage%2Fc5%2F1d%2F606acdb8ec1abe46e08d6b9859d5%2Fpreviews%2F1783-meta-image-1600.jpg&lr=172&nl=1&pos=17&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fget.wallhere.com%2Fphoto%2Ftypes-of-volcanic-eruptions-volcano-volcanic-landform-geological-phenomenon-shield-volcano-phenomenon-sky-stratovolcano-lava-dome-night-darkness-evening-lava-922086.jpg&lr=172&nl=1&pos=18&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fhappylove.top%2Fuploads%2Fposts%2F2023-05%2F1683500246_happylove-top-p-vulkan-v-okeane-pinterest-2.jpg&lr=172&nl=1&pos=19&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fi.pinimg.com%2Foriginals%2Fcf%2F99%2Fa6%2Fcf99a685fa55ac2c1dd03a1434bba85d.jpg&lr=172&nl=1&pos=20&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fmtdata.ru%2Fu21%2Fphoto99EB%2F20921733222-0%2Foriginal.jpeg&lr=172&nl=1&pos=22&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fupload.wikimedia.org%2Fwikipedia%2Fcommons%2Fthumb%2F4%2F48%2FAugustine_volcano_Jan_24_2006_-_Cyrus_Read.jpg%2F560px-Augustine_volcano_Jan_24_2006_-_Cyrus_Read.jpg&lr=172&nl=1&pos=23&rpt=simage&source=morda&text=%D0%92%D1%83%D0%BB%D0%BA%D0%B0%D0%BD%D1%8B",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fmasyamba.ru%2F%25D1%2581%25D0%25BE%25D0%25B2%25D0%25B0-%25D0%25BA%25D0%25B0%25D1%2580%25D1%2582%25D0%25B8%25D0%25BD%25D0%25BA%25D0%25B8%2F88-%25D0%25BA%25D0%25B0%25D1%2580%25D1%2582%25D0%25B8%25D0%25BD%25D0%25BA%25D0%25B8-%25D1%2581%25D0%25BE%25D0%25B2%25D1%258B-%25D0%25BA%25D1%2580%25D0%25B0%25D1%2581%25D0%25B8%25D0%25B2%25D1%258B%25D0%25B5.jpg&lr=172&nl=1&pos=1&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fwww.zastavki.com%2Fpictures%2Foriginals%2F2017Animals___Birds_Green-eyed_owl_sits_on_a_spruce_branch_113088_.jpg&lr=172&nl=1&pos=2&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fw.wallha.com%2Fws%2F12%2FCdW7fRDw.jpg&lr=172&nl=1&pos=3&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fwww.fonstola.ru%2Fimages%2F201504%2Ffonstola.ru_172117.jpg&lr=172&nl=1&pos=4&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Ffunik.ru%2Fwp-content%2Fuploads%2F2018%2F11%2Fc7b93869070a5674a1b0.jpg&lr=172&nl=1&pos=5&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Ffotobase.co%2Ffiles%2Fimg%2Fphoto%2Ffilin%2Ffilin-69.webp&lr=172&nl=1&pos=6&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fpbs.twimg.com%2Fmedia%2FFMeUV_jXEAIoAqX.jpg&lr=172&nl=1&pos=7&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fi.artfile.ru%2F2048x1365_734230_%5Bwww.ArtFile.ru%5D.jpg&lr=172&nl=1&pos=9&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=http%3A%2F%2Fcdn.culture.ru%2Fimages%2F3e9ad324-e368-55ad-a920-2b7da78e53e2&lr=172&nl=1&pos=10&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5",
            "https://yandex.ru/images/search?img_url=https%3A%2F%2Fimages.hdqwalls.com%2Fdownload%2Fowl-4k-do-3840x2400.jpg&lr=172&nl=1&pos=11&rpt=simage&source=morda&text=%D0%A1%D0%BE%D0%B2%D0%BE%D0%BE%D0%B1%D1%80%D0%B0%D0%B7%D0%BD%D1%8B%D0%B5"
        };
            var images = await DownloadImages(imageUrls);
            List<byte[]> imageList = new List<byte[]>();
            foreach (var img in images)
            {
                imageList.Add(img);
                imageList.Add(img);
            }
            Shuffle(imageList);
            List<int> originalIndices = new List<int>();
            for (int i = 0; i < imageList.Count; i++)
            {
                originalIndices.Add(i);
            }
            List<int> shuffledIndices = new List<int>(originalIndices);
            Shuffle(shuffledIndices);
            Console.WriteLine("Изначальные номера: " + string.Join(", ", originalIndices));
            Console.WriteLine("Перемешанные номера: " + string.Join(", ", shuffledIndices));

            Console.WriteLine("Задание 2");
            ReadStudentsFromFile("students.txt");
            RunProgram();

            Console.WriteLine("Задание 3");
            RunGrandmaHospitalProgram();

            Console.WriteLine("Задание 4");
            var graph = new Dictionary<int, List<int>>()
        {
            { 0, new List<int> { 1, 2 } },
            { 1, new List<int> { 0, 3, 4 } },
            { 2, new List<int> { 0, 4 } },
            { 3, new List<int> { 1, 5 } },
            { 4, new List<int> { 1, 2, 5 } },
            { 5, new List<int> { 3, 4 } }
        };

            int startNode = 0;
            int targetNode = 5;

            List<int> path = BFS(graph, startNode, targetNode);

            if (path != null)
            {
                Console.WriteLine($"Кратчайший путь от {startNode} до {targetNode}: {string.Join(" -> ", path)}");
            }
            else
            {
                Console.WriteLine($"Нет пути от {startNode} до {targetNode}");
            }
        }

        static List<int> BFS(Dictionary<int, List<int>> graph, int start, int target)
        {
            Queue<int> queue = new Queue<int>();
            Dictionary<int, int> predecessors = new Dictionary<int, int>();
            HashSet<int> visited = new HashSet<int>();

            queue.Enqueue(start);
            visited.Add(start);
            predecessors[start] = -1;

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                if (current == target)
                {
                    return GetPath(predecessors, start, target);
                }

                foreach (int neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                        predecessors[neighbor] = current;
                    }
                }
            }

            return null;
        }

        static List<int> GetPath(Dictionary<int, int> predecessors, int start, int target)
        {
            List<int> path = new List<int>();
            for (int at = target; at != -1; at = predecessors[at])
            {
                path.Add(at);
            }
            path.Reverse();
            return path;
        }
        static void RunProgram()
        {
            string option;
            do
            {
                // Отображение меню
                Console.WriteLine("\nМеню:");
                Console.WriteLine("a. Новый студент");
                Console.WriteLine("b. Удалить");
                Console.WriteLine("c. Сортировать");
                Console.WriteLine("d. Выход");
                Console.Write("Выберите опцию: ");

                option = Console.ReadLine();

                switch (option)
                {
                    case "a":
                        AddNewStudent();
                        break;

                    case "b":
                        DeleteStudent();
                        break;

                    case "c":
                        SortStudents();
                        break;

                    case "d":
                        Console.WriteLine("Выход из программы.");
                        break;

                    default:
                        Console.WriteLine("Неправильный ввод. Попробуйте снова.");
                        break;
                }
            } while (option != "d");
        }

        static void ReadStudentsFromFile(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    Student student = new Student
                    {
                        LastName = parts[0].Trim(),
                        FirstName = parts[1].Trim(),
                        YearOfBirth = int.Parse(parts[2].Trim()),
                        Exam = parts[3].Trim(),
                        Score = int.Parse(parts[4].Trim())
                    };

                    students.Add(student);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка чтения файла: " + ex.Message);
            }
        }

        static void AddNewStudent()
        {
            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Введите год рождения: ");
            int yearOfBirth = int.Parse(Console.ReadLine());

            Console.Write("Введите экзамен: ");
            string exam = Console.ReadLine();

            Console.Write("Введите баллы: ");
            int score = int.Parse(Console.ReadLine());

            Student student = new Student
            {
                LastName = lastName,
                FirstName = firstName,
                YearOfBirth = yearOfBirth,
                Exam = exam,
                Score = score
            };

            students.Add(student);
            Console.WriteLine("Студент добавлен успешно!");
        }

        static void DeleteStudent()
        {
            Console.Write("Введите фамилию и имя студента для удаления: ");
            string fullName = Console.ReadLine();

            string[] parts = fullName.Split(' ');

            if (parts.Length < 2)
            {
                Console.WriteLine("Неправильный ввод. Пожалуйста, введите фамилию и имя.");
                return;
            }

            string lastName = parts[0].Trim();
            string firstName = parts[1].Trim();

            Student studentToDelete = students.FirstOrDefault(s => s.LastName == lastName && s.FirstName == firstName);

            if (studentToDelete != null)
            {
                students.Remove(studentToDelete);
                Console.WriteLine("Студент удален успешно!");
            }
            else
            {
                Console.WriteLine("Студентов таких нет");
            }
        }

        static void SortStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Список студентов пуст.");
                return;
            }

            students = students.OrderBy(s => s.Score).ToList();

            Console.WriteLine("Студенты отсортированы по баллам:");
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.LastName} {student.FirstName} - {student.Score} баллов");
            }
        }
    }
}
