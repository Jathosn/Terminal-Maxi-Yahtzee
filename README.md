# Game flow

- `Enter` = throw six dices once. Press `Enter` again to select what to keep. An empty string means you'll throw all your dices again.
- `S` allows you to view your scoreboard at any time during your turn. If you've made any throws, it's logged below so you.
- `H` allows you to view the shorthand notations for entering results into your scoreboard.
- `E` allows you to end your turn at any point, no matter how many throws you've done. You can also end a turn before throwing anything. Throws not spent are added to the next game round.

# Rules
- Every scoreboard dictionary key has it's own rules for what's acceptable and what's not acceptable. If you try to put an invalid item into a key, the value 0 will be saved for that entry. The rules for what can be put where can be found below.

## Scoreboard key rules
- ones to sixes - only dicerolls with the correct value can be entered. For example, fives can not be stored to the `sixes` key.
- one pair - any pair combination. 11, 22, 33, 44, 55 or 66
- two pair - any two pair combinations. `5, 5, 6, 6` is valid. `6, 6, 6, 6` is not.
- three pairs - any three pair combinations. same rules as with two pairs, except you have to have three different pairs to score points.
- 3 same - 3 or more dices with the same value.
- 4 same - 4 or more dices with the same value.
- 5 same - 5 or more dices with the same value.
- small straight - `1, 2, 3, 4, 5`
- large straight - `2, 3, 4, 5, 6`
- full straight - `1, 2, 3, 4, 5, 6`
- hut 2+3 - any combination of a pair and 3 same. `5, 5, 6, 6, 6` is valid. `5, 5, 5, 5, 5` is not.
- house 3+3 - three of any two different dices.
- tower 2+4 - 4 same plus one pair that is not of the same value.
- chance - every dice value summarized
- maxi-yahtzee - 6 of the same dice value. The maxi-yahtzee is worth 100 points.

- bonus - The bonus value is hidden, but activated in the final calculation if the user scores 84 or more points in their ones to sixes values. getting 4 of each value from ones to sixes will give you the exact amount required to receive a bonus. The bonus increases your end score by 100.
