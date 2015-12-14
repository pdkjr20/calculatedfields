# Introduction #

In this tutorial we will create new calculated field **Distance in miles**<br>
And we will calculate this field only for activities that have average heart rate bigger than 120<br>
<br>
<h1>Steps</h1>

<ul><li>At first create new custom field <b>Distance in miles</b>. Set type to number, decimal places to 2, summarize to sum<br>
</li><li>Now go to settings page of Calculated Fields plugin<br>
</li><li>In <b>Calculated Custom Field</b> combobox pick <b>Distance in miles</b>
</li><li>Right click on expression and choose Activity -> Distance. Now we will edit expression.<br>
Because now it will only calculate distance in meters. (all of the data provided by plugins are in meters or in seconds. It's important to give users raw date, unchanged by units conversion. Because you are the ones, that can do anything with them)<br>
Ok if we want to see it in miles, we need to divide distance by 1609.344.<br>
So result expression will look like this: <b><code> {DISTANCE}/1609.344 </code></b><br><br>
</li><li>Ok but we wanted to calculate value only if distance is bigger that 5000m ...<br>
Let's do it:<br>
</li><li>Right click on <b>Condition</b> textbox. Pick Activity -> AVGHR<br>
Now we will edit condition to this: <b><code> {AVGGR} &gt; 120 </code></b><br>
Because we wanted to make calculation only if average hr is bigger than 120.</li></ul>

<ul><li>Now click <b>Add</b> button and we are done. You can go to activity reports, select activities and run <b>Edit -> Calculated Fields -> Calculate Values</b>.<br>
Or you can just select your new row in configuration and push button Calculate selected rows. And plugin will calculate your new field for all activities.<br>
</li><li>If you want you can push button <b>Test selected</b> rows. This will do calculation for 30 activities. But values will be not written in your logbook. So it's a nice help for testing of your calculations. If no error will be shown, you are probably ok.<br></li></ul>

Here is screenshot of configuration for your check if something gets wrong:<br>
<img><img src='http://img706.imageshack.us/img706/3269/distanceinmiles.png' /></img>

<br>
It's wise to use conditions on calculations if you want for example calculate something only on activities with some category.<br>Or only for activities that have some word in notes etc. Everything you need.<br>

Here are some more examples of conditions:<br>
<ul><li>Calculate only if distance is not 0:<b><code> {DISTANCE} != 0 </code></b>
</li><li>Calculate only if distance is 0:<b><code> {DISTANCE} == 0 </code></b>
</li><li>Calculate only if category of activity contains word Running:<b><code> {Category}.Contains("Running") </code></b>
</li><li>Calculate only if category of activity contains word Running and don't contains Trail:<b><code> {Category}.Contains("Running") &amp;&amp; !{Category}.Contains("Trail") </code></b>
</li><li>Calculate only if distance is not 0 AND average hr is greater than 120:<b><code> {DISTANCE} != 0 &amp;&amp; {AVGHR} &gt; 120} </code></b>
</li><li>Calculate only if distance is not 0 AND average hr is greater than 120:<b><code> {DISTANCE} != 0 &amp;&amp; {AVGHR} &gt; 120} </code></b>
</li><li>Calculate only if Trimp is bigger or equal as 100 OR Trimp is smaller than 200 (Note that Trimp is custom field created by Training Load plugin):<b><code> {TRIMP} &gt;= 100 || {TRIMP} &lt; 200 </code></b>
</li><li>Calculate only if you have a word <b>Regeneration</b> in Notes on activity:<b><code> {NOTES}.Contains("Regeneration") </code></b>
</li><li>Calculate only if activity has GPS track:<b><code> {HASGPSTRACK} </code></b>