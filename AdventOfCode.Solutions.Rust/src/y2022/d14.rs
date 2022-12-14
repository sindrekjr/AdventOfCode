use std::{collections::HashSet, iter};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day14::solve_part_one(input),
        Part::P2 => Day14::solve_part_two(input),
    }
}

#[derive(Copy, Clone, Debug, Eq, PartialEq, Hash)]
struct Position {
    x: usize,
    y: usize,
}

impl Position {
    fn from(pos_str: &str) -> Self {
        let (x, y) = pos_str.split_once(',').unwrap();
        Self {
            x: x.parse::<usize>()
                .expect(&format!("unable to parse x: {}", x)),
            y: y.parse::<usize>()
                .expect(&format!("unable to parse y: {}", y)),
        }
    }

    fn path_to(&self, other: &Position) -> HashSet<Position> {
        if self.x == other.x {
            let x = self.x;

            let range = if self.y < other.y {
                self.y + 1..other.y + 1
            } else {
                other.y..self.y + 1
            };

            iter::once(self.clone()).chain(range.map(|y| Position { x, y })).collect::<HashSet<Position>>()
        } else {
            let y = self.y;

            let range = if self.x < other.x {
                self.x + 1..other.x + 1
            } else {
                other.x..self.x + 1
            };

            iter::once(self.clone()).chain(range.map(|x| Position { x, y })).collect::<HashSet<Position>>()
        }
    }

    fn down(&self) -> Self {
        Self {
            x: self.x,
            y: self.y + 1,
        }
    }

    fn down_left(&self) -> Self {
        Self {
            x: self.x - 1,
            y: self.y + 1,
        }
    }

    fn down_right(&self) -> Self {
        Self {
            x: self.x + 1,
            y: self.y + 1,
        }
    }
}

const ENTRY: Position = Position { x: 500, y: 0 };

struct Day14;
impl Solution for Day14 {
    fn solve_part_one(input: String) -> String {
        let (abyss, mut set) = parse_rocks(&input);
        let mut sands = 0;

        loop {
            let mut finished = false;
            let mut sand = ENTRY;

            loop {
                if sand.y >= abyss {
                    finished = true;
                    break;
                }

                if !set.contains(&sand.down()) {
                    sand = sand.down();
                } else if !set.contains(&sand.down_left()) {
                    sand = sand.down_left();
                } else if !set.contains(&sand.down_right()) {
                    sand = sand.down_right();
                } else {
                    set.insert(sand);
                    sands += 1;
                    break;
                }
            }

            if finished {
                break;
            }
        }

        sands.to_string()
    }

    fn solve_part_two(_input: String) -> String {
        String::new()
    }
}

fn parse_rocks(input: &str) -> (usize, HashSet<Position>) {
    let mut abyssal_perimeter: usize = 500;
    let mut set = HashSet::new();

    for path in input.lines().map(|line| {
        line.split(" -> ")
            .map(Position::from)
            .collect::<Vec<Position>>()
    }) {
        for pos in path.windows(2).map(|path| {
            if path[0].y > abyssal_perimeter {
                abyssal_perimeter = path[0].y;
            }

            if path[1].y > abyssal_perimeter {
                abyssal_perimeter = path[1].y;
            }

            path[0].path_to(&path[1])
        }) {
            set.extend(pos);
        }
    }

    (abyssal_perimeter, set)
}
