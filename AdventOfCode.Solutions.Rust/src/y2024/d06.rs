use std::{collections::HashSet, fmt::Display};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day06::solve_part_one(input),
        Part::P2 => Day06::solve_part_two(input),
    }
}

#[derive(Copy, Clone)]
enum Direction {
    North,
    East,
    South,
    West,
}

struct Position {
    x: usize,
    y: usize,
    d: Direction,
}

impl Display for Position {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        let dir = match self.d {
            Direction::North => "N",
            Direction::East => "E",
            Direction::South => "S",
            Direction::West => "W",
        };

        write!(f, "{} ({},{})", dir, self.x, self.y)
    }
}

struct Day06;
impl Solution for Day06 {
    fn solve_part_one(input: String) -> String {
        let grid: Vec<Vec<char>> = input.lines().map(|line| line.chars().collect()).collect();

        let mut p = Position {
            x: 0,
            y: 0,
            d: Direction::North,
        };
        let mut obstacles = HashSet::new();
        for (y, row) in grid.iter().enumerate() {
            for (x, &ch) in row.iter().enumerate() {
                match ch {
                    '#' => {
                        obstacles.insert((x, y));
                    }
                    '^' => {
                        p = Position {
                            x,
                            y,
                            d: Direction::North,
                        }
                    }
                    '>' => {
                        p = Position {
                            x,
                            y,
                            d: Direction::East,
                        }
                    }
                    'v' => {
                        p = Position {
                            x,
                            y,
                            d: Direction::South,
                        }
                    }
                    '<' => {
                        p = Position {
                            x,
                            y,
                            d: Direction::West,
                        }
                    }
                    '.' => (),
                    _ => panic!(),
                };
            }
        }

        gallivant(&mut p, &obstacles, grid.len(), grid[0].len())
            .unwrap()
            .len()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let grid: Vec<Vec<char>> = input.lines().map(|line| line.chars().collect()).collect();

        let mut start = Position {
            x: 0,
            y: 0,
            d: Direction::North,
        };
        let mut obstacles = HashSet::new();
        for (y, row) in grid.iter().enumerate() {
            for (x, &ch) in row.iter().enumerate() {
                match ch {
                    '#' => {
                        obstacles.insert((x, y));
                    }
                    '^' => {
                        start = Position {
                            x,
                            y,
                            d: Direction::North,
                        }
                    }
                    '>' => {
                        start = Position {
                            x,
                            y,
                            d: Direction::East,
                        }
                    }
                    'v' => {
                        start = Position {
                            x,
                            y,
                            d: Direction::South,
                        }
                    }
                    '<' => {
                        start = Position {
                            x,
                            y,
                            d: Direction::West,
                        }
                    }
                    '.' => (),
                    _ => panic!(),
                };
            }
        }

        let height = grid.len();
        let width = grid[0].len();

        let mut pos = Position {
            x: start.x,
            y: start.y,
            d: start.d,
        };
        let path = gallivant(&mut pos, &obstacles, height, width).unwrap();

        path.iter()
            .filter(|(x, y)| {
                if *x == start.x && *y == start.y {
                    false
                } else {
                    let mut pos = Position {
                        x: start.x,
                        y: start.y,
                        d: start.d,
                    };
                    let mut obstacles = obstacles.clone();
                    obstacles.insert((*x, *y));

                    if gallivant(&mut pos, &obstacles, height, width) == None {
                        // println!("({}, {})", x, y);
                        true
                    } else {
                        false
                    }
                }
            })
            .count()
            .to_string()
    }
}

fn gallivant(
    pos: &mut Position,
    obstacles: &HashSet<(usize, usize)>,
    height: usize,
    width: usize,
) -> Option<HashSet<(usize, usize)>> {
    let mut loop_counter = 0;
    let mut finished = false;
    let mut visits = HashSet::new();
    while !finished {
        // println!("Position: {}", p);

        match pos.d {
            Direction::North => {
                for y in (0..pos.y).rev() {
                    if obstacles.contains(&(pos.x, y)) {
                        pos.y = y + 1;
                        pos.d = Direction::East;
                        break;
                    } else {
                        if !visits.insert((pos.x, y)) {
                            loop_counter += 1;
                        } else {
                            loop_counter = 0;
                        }
                        // println!("Add ({},{})", p.x, y);
                    }

                    if y == 0 {
                        finished = true;
                    }
                }
            }
            Direction::East => {
                for x in pos.x..width {
                    if obstacles.contains(&(x, pos.y)) {
                        pos.x = x - 1;
                        pos.d = Direction::South;
                        break;
                    } else {
                        if !visits.insert((x, pos.y)) {
                            loop_counter += 1;
                        } else {
                            loop_counter = 0;
                        }
                        // println!("Add ({},{})", x, p.y);
                    }

                    if x == width - 1 {
                        finished = true;
                    }
                }
            }
            Direction::South => {
                for y in pos.y..height {
                    if obstacles.contains(&(pos.x, y)) {
                        pos.y = y - 1;
                        pos.d = Direction::West;
                        break;
                    } else {
                        if !visits.insert((pos.x, y)) {
                            loop_counter += 1;
                        } else {
                            loop_counter = 0;
                        }
                        // println!("Add ({},{})", p.x, y);
                    }

                    if y == height - 1 {
                        finished = true;
                    }
                }
            }
            Direction::West => {
                for x in (0..pos.x).rev() {
                    if obstacles.contains(&(x, pos.y)) {
                        pos.x = x + 1;
                        pos.d = Direction::North;
                        break;
                    } else {
                        if !visits.insert((x, pos.y)) {
                            loop_counter += 1;
                        } else {
                            loop_counter = 0;
                        }
                        // println!("Add ({},{})", x, p.y);
                    }

                    if x == 0 {
                        finished = true;
                    }
                }
            }
        }

        if loop_counter > 1 && loop_counter >= visits.len() {
            return None;
        }
    }

    Some(visits)
}
