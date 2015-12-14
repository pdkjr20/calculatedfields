# Introduction #

Here you can see many examples of what can be done in expressions.<br>
If you are a programmer, just for your information, expressions are evaluated dynamic<br>
by C# compiler. So in fact you can write in expressions everything that you can write in C# as an one line expressions.<br>
<br>
<br>
<h1>Details</h1>
<ul><li>remember to encapsulate fields with {}, name of field is case insensitive.<br>
<b><code> {AvGPACe} </code></b>
</li><li>basic math<br>
<b><code> {DISTANCE} * 5 / 20 * 1.2343 - (500 + 20.3443) * 0.32443 </code></b>
</li><li>strings concatenations and combining strings with fields<br>
<b><code> "this is our test string, distance is: " + {DISTANCE} + " and pace is: " + {AVGPACE} </code></b>
</li><li>check if text contains some string, for example check if Category contains text Running<br>
<b><code> {CATEGORY}.Contains("Running") </code></b>
</li><li>you can use regular expressions !<br>
<b><code> Regex.Match({WEATHERNOTES}, "[0-9]*.[0-9]*").Value </code></b>
</li><li>You can combine as many fields as you want in one formula<br>
<b><code> {DISTANCE} / {AVGPACE} + 120 + {AVGHR} </code></b></li></ul>

<br>
and many many more