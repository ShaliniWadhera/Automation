# Automation
Automation of computer database and JavaScript alerts

# Dependency:
    Net core 3.1
    
# About Automation:
    The solution consist of three projects
    1. Automation Framework: which is the wrapper of selenium
    2. Computer Database: GUI tests related to computer DB
    3. JavaScriptAlerts: GUI test related to JS alerts
    
# How to run test

    $dotnet build
    $dotnet test

<pre>
Tests will run parallely from Computer Database and JavaScript alerts project. It is expected that <b>one test case will fail</b>
</pre>
    
# Sample test results:
    JavaScriptAlerts.Tests.Alert.CanVerifyJsAlert
    
       Opening browser Chrome
    ***********************************************
    * Can verify JS Alert
    ***********************************************
    
    1 - 'Open Heroku App'
        Opening 'https://the-internet.herokuapp.com/javascript_alerts'
    
    2 - 'Click for JS alerts'
        Clicked 'By.XPath: //*[@id='content'] //button[text()='Click for JS Alert']'
    
    3 - 'Verify alert text'
        Alert present = True
        Alert text = I am a JS Alert
        Alert Accepted
    
    
