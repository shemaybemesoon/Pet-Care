using interfacer;

var menu = new Menu();
Console.WriteLine(menu.UI());

while (menu.Running)
{
    var selector = Console.ReadLine();
    if (selector is null)
    {
        break;
    }

    menu.Redirector(selector);
}
