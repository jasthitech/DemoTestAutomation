Feature: PaymentsFunctionality

A short summary of the feature

@tag1
Scenario Outline: Verify funds transfer functionality

Given a user is on the Payments page
And the Everyday account has a balance greater than <transfer_amount>
And the Bills account has a balance less than <transfer_amount>
When the user transfers <transfer_amount> from the Everyday account to the Bills account
Then a transfer successful message should be displayed
And the current balance of the Everyday account should be reduced by <transfer_amount>
And the current balance of the Bills account should be increased by <transfer_amount>

Examples:
| transfer_amount |
| $500 |
| $600 |
| $700 |
