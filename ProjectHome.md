Plugin adds new features for Sporttracks 3. Actually it adds functionality for calculating your own values and filling them into custom fields. (Custom fields is standard feature of ST3, that enables user to add their user defined fields to application)

**Please note that plugin now needs .NET 3.5 (this can change in future)
If you have only version 2 you can download .NET 3.5 here:**<br>
<a href='http://www.microsoft.com/downloads/en/details.aspx?FamilyId=333325fd-ae52-4e35-b531-508d977d32a6&displaylang=en'>http://www.microsoft.com/downloads/en/details.aspx?FamilyId=333325fd-ae52-4e35-b531-508d977d32a6&amp;displaylang=en</a>

With this plugin you can define your own formulas, that will be calculated add filled into custom fields you choose.<br>
<br>
So you want to create your own formulas?<br>
<ul><li>You want to calculate Normalized graded pace?<br>
</li><li>You want to calculate something that is not in sporttracks?<br>
</li><li>You want to see distance, speed or pace in miles and in kilometers at once?<br>
</li><li>You want to create your own performance index?<br>
Than you are at right address, all of this can be done with this plugin.<br><br></li></ul>

Here is small example:<br>
Look at <b>Avg. HR/min: Active</b> formula (just selected row in settings)<br><br>
Formula is: <b><code> {ActiveAvgPace}/60 * {ActiveAvgHR} </code></b><br>
This means Average Pace from activity parts marked as active is divided by 60. Then multiplied by Average Heart Rate of active parts of activity.<br>
And as a result we have heart beats needed to run one kilometer.<br>
<img src='http://img835.imageshack.us/img835/5798/settingsq.png' width='100%' />

Here is <b>Avg. HR/min: Active</b> custom field calculated in activity reports on all activities:<br>
<br>
<img src='http://img8.imageshack.us/img8/5484/reportpy.png' width='100%' />

And of course with ST3 you have ability to create charts on your custom fields. And now filled with your own data automatically!<br>
<br>
<img src='http://img827.imageshack.us/img827/1083/chartj.png' width='100%' />

<br>
See Wiki section for tutorials:<br>
Here is link to basic tutorial<br>
<a href='http://code.google.com/p/calculatedfields/wiki/Tutorial1'>http://code.google.com/p/calculatedfields/wiki/Tutorial1</a>