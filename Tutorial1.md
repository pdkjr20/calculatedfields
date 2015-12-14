This tutorial will help you step by step to create your first formula with **Calculated Fields** plugin

# Introduction #

In this tutorial we will create new custom field **Adjusted Distance**.<br>
We will calculate <b>special</b> distance that will be normal distance of activity adjusted by 3%.<br><br>
For example some users know that their GPS is constantly showing distance shorter by 3% than in reality.<br>
So they want to show field with distance that is longer by 3%.<br>
<br>
<br>
<h1>Steps</h1>

<ul><li>First download this plugin from plugin catalogue or from this page in Downloads tab<br><br>
</li><li>Now we will create new custom field in ST<br>
Go to Logbook -> Properties -> Activity (because we want to create custom field that will be shown on activity)<br>
Here click button <b>Add Data Field</b>.<br>
Set name to <b>Adjusted Distance</b><br>
Set type to <b>Number</b>. We want to see distance as number right? This is important for charting too. We can't create create charts on custom field with type text<br>
Set decimal places as you want. I want to see 2 decimal places.<br>
Set summary method to <b>Sum</b>. I want to see sum of distances in activity reports when I group by week. Or just in summary row.<br><br></li></ul>

Ok your configuration may looks like this:<br>
<img src='http://img43.imageshack.us/img43/2420/customfieldsettings.png' />

Ok now we have created our new custom field. So let's go to Calculated Fields plugin settings. Settings -> Plugins -> Calculated Fields<br>
<br>
<ul><li>Now we will create our formula for Adjusted Distance<br>
First step will be to pick custom field, where our calculation will be written.<br>
So pick Adjusted Distance from <b>Calculated Custom Field</b> combobox.</li></ul>

In next step we need to create expression for calculation. This expression will be calculated and filled to our created custom field.<br>
Ok, we want to multiply distance by 1.03. This will make distance longer by 3%.<br><br>
Right click on <b>Expression</b> textbox<br>
<br>
And pick value Activity -> Distance. It may look like this:<br>
<img src='http://img201.imageshack.us/img201/7606/pick.png' />

Now we have in Expression this formula: <b><code> {DISTANCE} </code></b>. Ok but we want to multiply distance by 1.03. So edit expression to make it looking like this:<br>
<b><code> {DISTANCE}*1.03 </code></b><br>

<b>NOW CLICK Add BUTTON!</b> After this step we have created calculation for Adjusted Distance.<br>
And your configuration may looks like:<br>
<img src='http://img525.imageshack.us/img525/6813/adjdistancecreated.png' />

<ul><li>Ok we have created our custom field, created formula for calculation. But we still don't have any values calculated.<br>
There are two ways to do it:<br>
In settings page of plugin you can select your calculation row and push button Calculate selected rows. And this will calculate selected row for all activities in your logbook. It may take some time dependent on count of activities, calculations etc.<br>
<img src='http://img697.imageshack.us/img697/7357/calculateall.png' /></li></ul>

Or you can go to Daily activity view or Activity reports. Select activities for your calculation. And run action <b>Edit -> Calculated Fields -> Calculate Values</b><br>
After this plugin will calculate all expressions in your configuration that are active. But only for selected activities.<br>
<br>
<ul><li>Now if you want to go further. You can add custom field Adjusted Distance in Daily Activity or in Activity Reports. Make reports on it. Or you can add it to shown charts.<br>
For example here I made chart from my custom field Avg. HR/min Active:<br></li></ul>

<img src='http://img827.imageshack.us/img827/1083/chartj.png' />