int soucet;
string smrad = "";
Dictionary<string, string> smradosi = new Dictionary<string, string>();
while (true)
{
    soucet = 0;
    Console.WriteLine("\nZadejte jméno smraďocha:");
    string jmeno = Console.ReadLine();
    for (int i = 0; i < jmeno.Length; i++)
    {
        soucet += (int) jmeno[i];
    }
    if (soucet % 7 == 0)
    {
        smrad = "smrdí jako bolavá noha bezdomovce";
    }
    else if (soucet % 5 == 0)
    {
        smrad = "smrdí jako koňská řiť";
    }
    else if (soucet % 3 == 0)
    {
        smrad = "smrdí jako cibuláč";
    }
    else
    {
        smrad = "nesmrdí vůbec";
    }
    Console.WriteLine($"{jmeno} {smrad}.");
    smradosi.Add(jmeno,smrad);
    Console.WriteLine("\nTabulka smraďochů:\n------------------");
    foreach (KeyValuePair<string,string> smradoch in smradosi)
    {
        Console.WriteLine($"{smradoch.Key} \t {smradoch.Value}");
    }

}