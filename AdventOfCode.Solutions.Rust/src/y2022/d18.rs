use std::collections::HashSet;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day18::solve_part_one(input),
        Part::P2 => Day18::solve_part_two(input),
    }
}

#[derive(Eq, Hash, PartialEq)]
struct Coordinate {
    x: i32,
    y: i32,
    z: i32,
}

impl From<&str> for Coordinate {
    fn from(coor_str: &str) -> Self {
        let mut split = coor_str.split(',');

        Self {
            x: split.next().unwrap().parse::<i32>().unwrap(),
            y: split.next().unwrap().parse::<i32>().unwrap(),
            z: split.next().unwrap().parse::<i32>().unwrap(),
        }
    }
}

impl Coordinate {
    fn neighbours(&self) -> [Self; 6] {
        [
            Coordinate {
                x: self.x,
                y: self.y,
                z: self.z + 1,
            },
            Coordinate {
                x: self.x,
                y: self.y,
                z: self.z - 1,
            },
            Coordinate {
                x: self.x + 1,
                y: self.y,
                z: self.z,
            },
            Coordinate {
                x: self.x - 1,
                y: self.y,
                z: self.z,
            },
            Coordinate {
                x: self.x,
                y: self.y + 1,
                z: self.z,
            },
            Coordinate {
                x: self.x,
                y: self.y - 1,
                z: self.z,
            },
        ]
    }
}

struct Day18;
impl Solution for Day18 {
    fn solve_part_one(input: String) -> String {
        let coordinates: HashSet<Coordinate> = input.lines().map(Coordinate::from).collect();

        coordinates
            .iter()
            .fold(coordinates.len() * 6, |acc, coordinate| {
                acc - coordinate
                    .neighbours()
                    .iter()
                    .filter(|neighbour| coordinates.contains(neighbour))
                    .count()
            }).to_string()
    }

    fn solve_part_two(input: String) -> String {
        String::new()
    }
}
