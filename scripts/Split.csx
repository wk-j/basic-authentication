var a = "Basic 1234";

var rs = a.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
Console.WriteLine(rs[1] == "1234");