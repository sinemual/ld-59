using UnityEngine;

namespace Client.DevTools.MyTools
{
    public static class BotNames
    {
        public static string[] names =
        {
            "Allison", "Arthur", "Ana", "Alex", "Arlene", "Alberto", "Aki", "Ayumi", "Akane",
            "Barry", "Bertha", "Bill", "Bonnie", "Bret", "Beryl",
            "Chantal", "Cristobal", "Claudette", "Charley", "Cindy", "Chris", "Chiaki",
            "Dean", "Dolly", "Danny", "Danielle", "Dennis", "Debby",
            "Erin", "Edouard", "Erika", "Earl", "Emily", "Ernesto", "Emi", "Etsuko",
            "Felix", "Fay", "Fabian", "Frances", "Franklin", "Florence",
            "Gabielle", "Gustav", "Grace", "Gaston", "Gert", "Gordon",
            "Humberto", "Hanna", "Henri", "Hermine", "Harvey", "Helene", "Hitomi",
            "Iris", "Isidore", "Isabel", "Ivan", "Irene", "Isaac", "Itoe",
            "Jerry", "Josephine", "Juan", "Jeanne", "Jose", "Joyce", "Junko",
            "Karen", "Kyle", "Kate", "Karl", "Katrina", "Kirk", "Kumiko", "Kaori", "Kazuko",
            "Lorenzo", "Lili", "Larry", "Lisa", "Lee", "Leslie",
            "Michelle", "Marco", "Mindy", "Maria", "Michael", "Miharu", "Michiyo", "Miyuki", "Miwa", "Miyako", "Mieko",
            "Noel", "Nana", "Nicholas", "Nicole", "Nate", "Nadine", "Nanaho", "Naoko",
            "Olga", "Omar", "Odette", "Otto", "Ophelia", "Oscar",
            "Pablo", "Paloma", "Peter", "Paula", "Philippe", "Patty",
            "Rebekah", "Rene", "Rose", "Richard", "Rita", "Rafael", "Reina", "Rie",
            "Sebastien", "Sally", "Sam", "Shary", "Stan", "Sandy", "Sayuri", "Sachiko",
            "Tanya", "Teddy", "Teresa", "Tomas", "Tammy", "Tony", "Toshie",
            "Van", "Vicky", "Victor", "Virginie", "Vince", "Valerie",
            "Yumi", "Youko",
            "Wendy", "Wilfred", "Wanda", "Walter", "Wilma", "William"
        };

        public static string GetRandom()
        {
            return names[Random.Range(0, names.Length)];
        }
    }
}