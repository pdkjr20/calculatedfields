# Introduction #

This tutorial will describe two powerful features of Calculated Fields plugin for analyze.

**Range feature**:<br>
Do you want to find your average pace when having HR between 150-180 in activity?<br>
Do you want to find your average heart rate when running in pace between 5:00-5:30?<br>
Exactly for this is Range feature designed.<br>
<br>
<b>Peak feature</b>:<br>
Do you want to find your fastest kilometer in activity?<br>
Or your maximal power output for 30 seconds?<br>
Or you want to find your fastest 300 seconds in activity?<br>
Peak feature is here for help.<br>
<br>
<b>Important</b>:<br>
Results returned by Range and Peak feature are dependant on your smoothing settings of used datatracks in ST settings.<br>
And you can influence accuracy of results by settings smaller resolution of DataTrack<br>
So 1000 milliseconds is for example fast standard with relative accurate results for DataTrack feature. But with big activities with many pauses result will be few seconds out.<br>
But with 100 milliseconds resolution it will be much more accurate. Of course the calculation will be slower. So it's up to you what you will choose. Just experiment with resolution settings and choose what you want.<br>

<h1>Details</h1>

<b>Range feature examples and description</b>:<br>

Here is example formula for finding how much time you have been in HR between 150-180:<br>
<b><code> {RANGEELAPSED(HR,150,180)} </code></b><br>
So first is RANGE, right after range you define return parameter of formula. So in this example we want to return elapsed time in seconds. First parameter is HR. This will tell plugin that you want to filter range on activity on HR. Second and third parameter is number that defines lower and upper bound of range.<br>
<br>
<BR><br>
<br>
<br>
<br>
<br>
<BR><br>
<br>
<br>
Formula for calculating your average pace when having HR between 150-170:<br>
<b><code> {RANGEPACE(HR,150,170)} </code></b><br>
So we want to return PACE and define range on HR field and lower bound is 150 and upper 170.<br>
<br>
Formula for calculating your average pace when having HR between 150-170 <b>(but only for active parts of activity)</b>:<br>
<b><code> {ACTIVERANGEPACE(HR,150,170)} </code></b><br>
<br>
Here is list of usable fields (they can be used as return values and range field too):<br>
<ul><li>ELAPSED<br>
</li><li>DISTANCE<br>
</li><li>HR<br>
</li><li>PACE<br>
</li><li>SPEED<br>
</li><li>ELEVATION<br>
</li><li>GRADE<br>
</li><li>CADENCE<br>
</li><li>POWER<br>
</li><li>CLIMBSPEED<br>
<br>
<b>Peak feature examples and description</b>:<br></li></ul>

Now we want to find our fastest 1000meters (pretty exciting, we have High score plugin formula :) ):<br>
<b><code> {MINPEAKDISTANCE(Elapsed,1000)} </code></b><br>
Now we want to find minimal time for 1000 meters in our activity. (so we want to find MINPEAK). We want to find peak on interval of DISTANCE. First parameter specifies peak type. Second parameter specifies interval value so in this example 1000 meters.<br>
Ok now we want to find fastest 300 seconds of activity:<br>
<b><code> {MAXPEAKTIME(Distance,300)} </code></b><br>
So now we want to find maximal distance that have been taken in 300 seconds. (so we want to find MAXPEAK). We want to find peak on interval of TIME. We want to return result as a DISTANCE. And we want to find peak on 300 seconds interval.<br>
Ok do you want to find your power peak on 20 seconds?:<br>
<b><code> {MAXPEAKTIME(Power,20)} </code></b><br>
We want to find maximal power. (so max peak). Interval is of type TIME. Result and peak will be Power. And seconds parameter is 20 seconds. That's an interval for our peak.<br>
etc.<br>
<br><br>
Ok but what if we want to see average cadence of our 30 seconds power peak?<br>
Here it is. You can use second optional parameter for return type:<br>
<b><code> {MAXPEAKTIME(Power,Cadence,30)} </code></b><br>
So now we still want to find peak of power for time of 30 seconds. But we don't want to return as a result power. But we want to know average cadence in these 30 seconds.<br>
So we specify our second optional parameter Cadence. (it's a return type).<br>
Of course you can return for example average HR for these 30 seconds:<br>
<b><code> {MAXPEAKTIME(Power,HR,30)} </code></b><br>
<br>


You can specifiy this combinations of PEAKS:<br>
<ul><li>MINPEAKTIME<br>
</li><li>MAXPEAKTIME<br>
</li><li>MINPEAKDISTANCE<br>
</li><li>MAXPEAKDISTANCE</li></ul>

And as a peak type or result type you can define these fields:<br>
<ul><li>ELAPSED<br>
</li><li>DISTANCE<br>
</li><li>HR<br>
</li><li>PACE<br>
</li><li>SPEED<br>
</li><li>ELEVATION<br>
</li><li>GRADE<br>
</li><li>CADENCE<br>
</li><li>POWER<br>
</li><li>CLIMBSPEED</li></ul>

Other examples:<br>
Fastest 2km only on activities with GPS Track:<br>
<b><code> {MINPEAKDISTANCE(Elapsed,2000)} </code></b> Condition: <b><code> {HASGPSTRACK} </code></b><br>
Power peak for 1000 meters:<br>
<b><code> {MAXPEAKDISTANCE(Power,1000)} </code></b><br>
HR Peak for 30 seconds:<br>
<b><code> {MAXPEAKTIME(HR,30)} </code></b><br>
Average HR of my fastest 1000meters:<br>
<b><code> {MINPEAKDISTANCE(Elapsed,HR,1000)} </code></b><br>