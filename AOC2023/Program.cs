// See https://aka.ms/new-console-template for more information
using AOC2023;
using System.Diagnostics;

//var ab = 'a';
var ab = 'a';
var day = 14;
var lines = Parser.ParseInput(day, ab);
var dayObject = DayFactory.CreateDayObject(day);
var result = dayObject.Run(ab, lines);
Printer.Print(result);
