use std::collections::HashSet;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day18::solve_part_one(input),
        Part::P2 => Day18::solve_part_two(input),
    }
}

#[derive(Clone, Copy, Debug, Eq, Hash, PartialEq)]
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
            })
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let mut minimum = Coordinate {
            x: i32::MAX,
            y: i32::MAX,
            z: i32::MAX,
        };

        let mut maximum = Coordinate {
            x: i32::MIN,
            y: i32::MIN,
            z: i32::MIN,
        };

        let cubes: HashSet<Coordinate> = input
            .lines()
            .map(|line| {
                let coordinate = Coordinate::from(line);

                if minimum.x > coordinate.x {
                    minimum.x = coordinate.x;
                }

                if maximum.x < coordinate.x {
                    maximum.x = coordinate.x;
                }

                if minimum.y > coordinate.y {
                    minimum.y = coordinate.y;
                }

                if maximum.y < coordinate.y {
                    maximum.y = coordinate.y;
                }

                if minimum.z > coordinate.z {
                    minimum.z = coordinate.z;
                }

                if maximum.z < coordinate.z {
                    maximum.z = coordinate.z;
                }

                coordinate
            })
            .collect();

        minimum.x -= 1;
        minimum.y -= 1;
        minimum.z -= 1;
        maximum.x += 1;
        maximum.y += 1;
        maximum.z += 1;

        let mut filled = HashSet::new();
        let mut outside = vec![minimum];

        let mut surface = 0;
        while let Some(coor) = outside.pop() {
            if filled.contains(&coor) {
                continue;
            }

            for neighbour in coor.neighbours() {
                if neighbour.x >= minimum.x
                    && neighbour.x <= maximum.x
                    && neighbour.y >= minimum.y
                    && neighbour.y <= maximum.y
                    && neighbour.z >= minimum.z
                    && neighbour.z <= maximum.z
                {
                    if cubes.contains(&neighbour) {
                        surface += 1;
                    } else {
                        outside.insert(0, neighbour);
                    }
                }
            }

            filled.insert(coor);
        }

        surface.to_string()
    }
}
