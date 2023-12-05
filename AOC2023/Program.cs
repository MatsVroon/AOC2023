// See https://aka.ms/new-console-template for more information
using AOC2023;

//var ab = 'a';
var ab = 'b';
var day = 5;
var lines = Parser.ParseInput(day, ab);
var dayObject = DayFactory.CreateDayObject(day);
var result = dayObject.Run(ab, lines);
Printer.Print(result);
