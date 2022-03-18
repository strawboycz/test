int soucet,pocet;
string smrad = "";
float prumer;
Dictionary<string, string> smradosi = new Dictionary<string, string>();
while (true)
{
    soucet = 0;
    Console.WriteLine("\nZadejte jméno smraďocha:");
    string jmeno = Console.ReadLine();
    for (pocet = 0; pocet < jmeno.Length; pocet++)
    {
        soucet += (int) jmeno[pocet];
    }
    prumer = (float) soucet /pocet;
    if ((int)prumer % 7 == 0)
    {
        smrad = "smrdí jako bolavá noha bezdomovce";
    }
    else if ((int)prumer % 5 == 0)
    {
        smrad = "smrdí jako koňská řiť";
    }
    else if ((int)prumer % 3 == 0)
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