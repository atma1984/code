﻿using System;
using static System.Console;


string name = "Samantha Jones";
// использование метода Substring
int lengthOfFirst = name.IndexOf(' ');
int lengthOfLast = name.Length - lengthOfFirst - 1;
string firstName = name.Substring(
startIndex: 0,
length: lengthOfFirst);
string lastName = name.Substring(
startIndex: name.Length - lengthOfLast,
length: lengthOfLast);
WriteLine($"First name: {firstName}, Last name: {lastName}");
// использование интервалов
ReadOnlySpan<char> nameAsSpan = name.AsSpan();
ReadOnlySpan<char> firstNameSpan = nameAsSpan[0..lengthOfFirst];
ReadOnlySpan<char> lastNameSpan = nameAsSpan[^lengthOfLast..^0];
WriteLine("First name: {0}, Last name: {1}",arg0: firstNameSpan.ToString(),arg1: lastNameSpan.ToString());



WriteLine(new string('-', 50));
string strname = "abcdefg";
ReadOnlySpan<char> strnameAsSpan = strname.AsSpan();
ReadOnlySpan<char> strfirstNameSpan = strnameAsSpan[0..4];
ReadOnlySpan<char> strlastNameSpan = strnameAsSpan[^5..^0];
WriteLine("1 - {0},2 - {1}", strfirstNameSpan.ToString(), strlastNameSpan.ToString());

