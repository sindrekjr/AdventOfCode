use std::{collections::HashSet, iter};

use crate::core::{Part, Solution};

use super::coor::Position;

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day14::solve_part_one(input),
        Part::P2 => Day14::solve_part_two(input),
    }
}

impl Position<usize> {
    fn from(pos_str: &str) -> Self {
        let (x, y) = pos_str.split_once(',').unwrap();
        Self {
            x: x.parse::<usize>()
                .expect(&format!("unable to parse x: {}", x)),
            y: y.parse::<usize>()
                .expect(&format!("unable to parse y: {}", y)),
        }
    }

    fn path_to(&self, other: &Position<usize>) -> HashSet<Position<usize>> {
        if self.x == other.x {
            let x = self.x;

            let range = if self.y < other.y {
                self.y + 1..other.y + 1
            } else {
                other.y..self.y + 1
            };

            iter::once(self.clone())
                .chain(range.map(|y| Position { x, y }))
                .collect::<HashSet<Position<usize>>>()
        } else {
            let y = self.y;

            let range = if self.x < other.x {
                self.x + 1..other.x + 1
            } else {
                other.x..self.x + 1
            };

            iter::once(self.clone())
                .chain(range.map(|x| Position { x, y }))
                .collect::<HashSet<Position<usize>>>()
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

const ENTRY: Position<usize> = Position { x: 500, y: 0 };

struct Day14;
impl Solution for Day14 {
    fn solve_part_one(input: String) -> Option<String> {
        let (abyssal_perimeter, mut set) = parse_rocks(&input);

        let mut sands = 0;
        loop {
            let mut finished = false;
            let mut sand = ENTRY;

            loop {
                if sand.y >= abyssal_perimeter {
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

        Some(sands.to_string())
    }

    fn solve_part_two(input: String) -> Option<String> {
        let (lowest, mut set) = parse_rocks(&input);
        let floor = lowest + 2;

        let mut sands = 0;
        loop {
            let mut sand = ENTRY;

            loop {
                if sand.y + 1 == floor {
                    set.insert(sand);
                    sands += 1;
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

            if set.contains(&ENTRY) {
                break;
            }
        }

        Some(sands.to_string())
    }
}

fn parse_rocks(input: &str) -> (usize, HashSet<Position<usize>>) {
    let mut lowest: usize = 0;
    let mut set = HashSet::new();

    for path in input.lines().map(|line| {
        line.split(" -> ")
            .map(Position::from)
            .collect::<Vec<Position<usize>>>()
    }) {
        for pos in path.windows(2).map(|path| {
            if path[0].y > lowest {
                lowest = path[0].y;
            }

            if path[1].y > lowest {
                lowest = path[1].y;
            }

            path[0].path_to(&path[1])
        }) {
            set.extend(pos);
        }
    }

    (lowest, set)
}
