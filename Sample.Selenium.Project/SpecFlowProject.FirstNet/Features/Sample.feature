Feature: Sample_Feature


@UAT
Scenario: Invalid credentials
	Given I Launch saucedemo url
	When I login to saucedemo using username 'standard_user'and password 'abcdefgh'
	Then the login will fail and show the error

@UAT
Scenario: Login with valid credentials
	Given I Launch saucedemo url
	When I login to saucedemo using username 'standard_user'and password 'secret_sauce'
	Then I should see the dashboard
	
@UAT
 #Taking test data from feature file
Scenario: Add the items to cart
	Given I Launch saucedemo url
	When I login to saucedemo using username 'standard_user'and password 'secret_sauce'
	Then I should see the dashboard
	And I select the Items from low price
	And I add the first 4 cheapest items into the cart
	And I Navigate to cart and see 4 items in the cart
	And I check the total cost and remove products untill the total cost will be less than 50
	And I click on checkout

@UAT
 #Taking test data from Json Json file
Scenario: Add the items to cart using config test data
	Given I Launch saucedemo url
	When I login to saucedemo using credentials
	Then I should see the dashboard
	And I select the Items from low price
	And I add the first 6 cheapest items into the cart
	And I Navigate to cart and see 6 items in the cart
	And I check the total cost and remove products untill the total cost will be less than 50
	And I click on checkout
	