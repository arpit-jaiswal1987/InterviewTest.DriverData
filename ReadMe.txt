This document illustrates the project implementation details.

Task 1.1 Delivery Driver Analyser
Task 1.2 Formula One Driver Analyser
Task 1.3 Getaway Driver Analyser

a) Created a new entity AnalyserSetting. This holds the settings that analyser requires for rating calculation. This contains StartOfDay, EndOfDay and SpeedLimit.
b) Created AnalyserHelper which has methods to get the valid list of periods and undocumented periods based on the AnalyserSettings, and also the method to calculate the overall weighted rating.
c) Each Analyser has the code to get the valid and undocumented periods based on analyser settings. Now calculate the rating for each period (this vary for each analyser). At the end call the helper to calculate the overall rating.
d) Followed Test Driven Development approach, hence added the test cases for scenarios and then added code to pass these test cases.

Task 1.4 Penalise Faulty Recording

a) Added 2 more properties into AnalyserSetting (ApplyUnDocumentedPenaltyFlag and UndocumentedPenalty).
b) For the test cases written for Task 1.1, 1.2, 1.3 need not to change, as the default value of ApplyUnDocumentedPenaltyFlag is false, and penalty will not be applied.
c) This approach will benefit us, as we do not need to change the code which were using these analysers before.
d) New test cases are written for testing the penalty functionality.
e) Based on the flag ApplyUnDocumentedPenaltyFlag, the rating is multiplied by UndocumentedPenalty (0.5 in our case). Penalty can be changed from the setting and need not to change the code.

Task 2.0 Better Analyser Lookup

a) Used dependancy inversion to derive the analyser to be used.
b) For this we have registered all types of analysers that we support.{friendly, delivery_driver, delivery_driver_with_penalty, formula_one_driver, formula_one_driver_with_penalty, getaway_driver and getaway_driver_with_penalty} 
c) And then base on the parameters we derive which analyser to return.
d) This will help us to switch the Analyser implementation from the registered types and will not have to make changes in code anywhere else.
e) Written test cases to test if the analyser lookup is working appropriately.

Task 3.0 Canned Data Schemanned Data

a) Designed a json file for the canned data.
b) These json files reside in the in the folder "HistoryDataFiles".
c) This folder needs to be copied to a location and this location need to be configured in the app.config of Test and Console.
d) Written File Reader and Serialization Helpers to read the file from the configured location, and then deserialize it into history data (List<Period>).
e) Lookups have been created for these helpers, so that if we want to change the Serializer from Json to Xml there will change in Lookup only, similarly if we want to replace the File Reader with http File reader, the change will be only in the lookup.
f) Written test cases for File Reader and Serialization helper.
g) Written test case ShouldYieldCorrectValuesCannedDataFromFile in each Analyser's test.

Task 4.0 Improve the tests

We have followed Test Driven Development, we have written test cases for scenarios and then written code to pass them.