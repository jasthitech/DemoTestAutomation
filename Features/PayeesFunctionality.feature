Feature: PayeesFunctionality

A short summary of the feature

@tag1
Scenario: Verify navigation to Payees page using the navigation menu
Given I am on the BNZ demo application home page
When I click on the 'Menu' option
And I click on the 'Payees' option in the navigation menu
Then the Payees page should be loaded successfully.

Scenario: Verify you can add new payee in the Payees page
Given I am on the Payees page
When I click the 'Add' button
And I enter the payee details (name, account number)
And I click the 'Add' button on the model
Then I should see the message 'Payee added'
And the payee should be added to the list of payees

Scenario: Verify payee name is a required field
Given I am on the Payees page
When I click the 'Add' button
And I click the 'Add' button on the model
Then I should see validation errors
When I populate the mandatory fields
Then the validation errors should disappear

Scenario: Verify that payees can be sorted by name
Given I am on the Payees page
And there are existing payees in the list
When the user adds a new payee
Then the payee list should be sorted in ascending order by default
And the user clicks the Name header
Then the payee list should be sorted in alphabetical order by name.


