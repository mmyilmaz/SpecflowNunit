
Feature: Users should be able to login
  
  #Scenario: Login as a driver
  #  Given the user is on the login pagee
  #  When the user enters the driver information
  #  Then the user should be able to login
  Scenario Outline: Login as various
    Given the user is on the login pageee
    When user enters "<username>" and "<password>"
    Then  user should be able to login

    Examples: 
    | username        | password    |
    | user1           | UserUser123 |
    | salesmanager101 | UserUser123 |
    | storemanager85  | UserUser123 |
