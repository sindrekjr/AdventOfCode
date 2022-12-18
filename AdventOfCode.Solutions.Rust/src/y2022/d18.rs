use std::collections::HashSet;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day18::solve_part_one(input),
        Part::P2 => Day18::solve_part_two(input),
    }
}

#[derive(Debug, Eq, Hash, PartialEq)]
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

    fn neighbours_incremental(&self, radius: u16) -> Vec<Vec<Coordinate>> {
        let mut neighbours = vec![];

        for x in -1..2 {
            for y in -1..2 {
                for z in -1..2 {
                    if (x == 0 && y == 0 && z == 0) || (x == y || y == z || x == z) {
                        continue;
                    }

                    let mut direction = vec![];

                    let mut start = Coordinate {
                        x: self.x,
                        y: self.y,
                        z: self.z,
                    };

                    for _ in 0..radius {
                        start = Coordinate {
                            x: start.x + x,
                            y: start.y + y,
                            z: start.z + z,
                        };

                        let current = Coordinate {
                            x: start.x,
                            y: start.y,
                            z: start.z,
                        };

                        direction.push(current);
                    }

                    neighbours.push(direction);
                }
            }
        }

        neighbours
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
        let mut air = HashSet::new();
        let cubes: HashSet<Coordinate> = input.lines().map(Coordinate::from).collect();

        for cube in &cubes {
            for maybe_air in cube.neighbours() {
                println!("{:?}", maybe_air);
                println!("{:?}", maybe_air.neighbours_incremental(4));

                if maybe_air
                    .neighbours_incremental(30)
                    .iter()
                    .all(|direction| direction.iter().any(|coor| cubes.contains(coor)))
                {
                    air.insert(maybe_air);
                }
            }
        }

        println!("{:?}", air);

        cubes
            .iter()
            .fold(cubes.len() * 6, |acc, coordinate| {
                acc - coordinate
                    .neighbours()
                    .iter()
                    .filter(|neighbour| cubes.contains(neighbour) || air.contains(neighbour))
                    .count()
            })
            .to_string()
    }
}
