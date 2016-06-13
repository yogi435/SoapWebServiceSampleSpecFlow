Feature: SpecFlowFeature1
	In order to check the weather in my city
	As a  User
	I want to get the weather report

@mytag
Scenario: Get Weather Report
	Given I have valid weather request
	And I have "CityName" as "Eglinton / Londonderr"
	And I have "CountryName" as "United Kingdom"
	When I request for weather report
	
Scenario Outline: Weather Reports for multiple cities
	Given I have valid weather request
	And I have "CityName" as "<CityName>"
	And I have "CountryName" as "<CountryName>"
	When I request for weather report
	Then I verify the Status as "<Status>"
	#And I Login to "https://SomeWebSite.com"

	Examples: 
	| CityName             | CountryName    | Status  |
	| Birmingham / Airport | United Kingdom | Success |
	| Staverton Private    | United Kingdom | Success |
	| Coventry Airport     | United Kingdom | Success |
	| Manchester Airport   | United Kingdom | Success |