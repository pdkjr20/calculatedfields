# Introduction #

This tutorial will guide you step by step with Trails feature of plugin.

Do you know Trails plugin? Calculated Fields plugin is integrated
with Trails plugin. So you can easily calculate your pace, time etc
for your trail created in Trails plugin.
And fill these values to your custom fields.

With this feature you have ability to create charts from your trails data.


# Details #

  * Ok we will first check if our integration with Trails plugin is enabled. Go to settings page of plugin and see text in upper right corner. If there is written that it's enabled integration is ok.<br>
<img><img src='http://img340.imageshack.us/img340/7444/integration.png' /></img><br><br>
If no please download latest build of Trails plugin and install it:<br>
<ul><li>First step is to create your trail in Trails plugin. It's important to remember it's name. In this example I have created trail with name <b>PernekMain</b>
<img><img src='http://img827.imageshack.us/img827/5477/trailsd.png' /></img></li></ul>

<ul><li>I have created bunch of custom fields for this trail like: Pernek Avg HR, Pernek Avg Pace, Pernek Main Distance, Pernek Main Time, Pernek Main Max HR. And set type of custom fields as necessary. Number for distance, elapsed time for pace etc. As you want.<br>
</li><li>Now I go to settings page of Calculated Fields plugin. And create here these formulas (of course you fill fill your trail name as first parameter, number 1, second parameter tell plugin which lap of trail to fill, because you can repeat your trail many times in one activity, so if you want to see 2 lap of that trail just replace 1 with 2 etc):<br>
Pernek Avg HR - <b><code> {TrailAvgHR(PernekMain,1)} </code></b><br>
Pernek Avg Pace - <b><code> {TrailAvgPace(PernekMain,1)} </code></b><br>
Pernek Main Distance - <b><code> {TrailDistance(PernekMain,1)} </code></b><br>
Pernek Main Max HR - <b><code> {TrailMaxHR(PernekMain,1)} </code></b><br>
Pernek Main Time - <b><code> {TrailTime(PernekMain,1)} </code></b><br>
</li><li>Now I have all complete so I can calculate trails on all my activities<br>
<br>
And here is nice result:<br>
<img><img src='http://img299.imageshack.us/img299/853/trailreport.png' /></img>
<img><img src='http://img844.imageshack.us/img844/4461/trailchart.png' /></img>