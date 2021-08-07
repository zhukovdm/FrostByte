# Frost Byte

## Introduction

A .NET implementation of the single-player arcade game heavily inspired by
ZX Spectrum [FrostByte](https://spectrumcomputing.co.uk/entry/1894/ZX-Spectrum/Frost_Byte).

![gui](./assets/images/gui.png)

## Gameplay

You control the `Hero`, yellow twisted rectangle with small cross in the upper
right corner. The `Hero` has `Hearts` (amount shall not drop below **1**,
otherwise loss) and `Bullets` (use it to fight enemies).

The object of the game is to collect all friends of the `Hero` captured by
enemies. `Friends` look like the `Hero`, but without yellow cross above.

Press `Enter` key button to start new game and to repeat it after final state
is achieved. Final states are considered either win or loss. The game could be
paused at any time by pressing `Pause` button and resumed by repeated
pressing. The player could exit the game by pressing `Esc`.

The `Hero` moves upon pressing `Left` or `Right`. It could jump, press `Up`.
Jump is interrupted upon `Left`, `Right` or `Down`. The `Hero` falls until
floor is reached. Use `w`, `a`, `s` or `d` to let the `Hero` fire in a
respective direction. If a bullet reaches an enemy, the enemy disappears.

The game is a multi-level maze with enemies. Use portals to reach another
levels. Portals are wall blocks with numbers on it, from **1** to **4**. There
are several kinds of enemies. Collision of the `Hero` and any enemy reduces
amount of `Hearts` by one. Moves of a `Jellyfish` are randomized, other
species move in a well defined directions. The `Skull` is very dangerous, it
jumps onto the `Hero` with probability **0.2** if a bullet or the `Hero`
itself is detected near a `Skull`.
