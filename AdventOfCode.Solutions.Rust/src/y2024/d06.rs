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

impl From<char> for Direction {
    fn from(ch: char) -> Self {
        match ch {
            '^' => Direction::North,
            '>' => Direction::East,
            'v' => Direction::South,
            '<' => Direction::West,
            _ => panic!("Invalid direction character"),
        }
    }
}

struct Guard {
    x: usize,
    y: usize,
    d: Direction,
}

impl Guard {
    fn rotate(&mut self) {
        self.d = match self.d {
            Direction::North => Direction::East,
            Direction::East => Direction::South,
            Direction::South => Direction::West,
            Direction::West => Direction::North,
        }
    }
}

impl Display for Guard {
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

        let (obstacles, x, y, d) = parse_grid(&grid);
        let mut guard = Guard { x, y, d };

        gallivant(&mut guard, &obstacles, grid.len(), grid[0].len())
            .unwrap()
            .len()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let grid: Vec<Vec<char>> = input.lines().map(|line| line.chars().collect()).collect();

        let (obstacles, start_x, start_y, start_d) = parse_grid(&grid);
        let mut first = Guard {
            x: start_x,
            y: start_y,
            d: start_d,
        };

        let height = grid.len();
        let width = grid[0].len();
        gallivant(&mut first, &obstacles, height, width)
            .unwrap()
            .iter()
            .filter(|(x, y)| {
                if *x == start_x && *y == start_y {
                    false
                } else {
                    let mut guard = Guard {
                        x: start_x,
                        y: start_y,
                        d: start_d,
                    };
                    let mut obstacles = obstacles.clone();
                    obstacles.insert((*x, *y));

                    if gallivant(&mut guard, &obstacles, height, width) == None {
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

fn parse_grid(grid: &Vec<Vec<char>>) -> (HashSet<(usize, usize)>, usize, usize, Direction) {
    grid.iter().enumerate().fold(
        (HashSet::new(), 0, 0, Direction::North),
        |(obstacles, start_x, start_y, start_d), (y, row)| {
            row.iter().enumerate().fold(
                (obstacles, start_x, start_y, start_d),
                |(mut obstacles, start_x, start_y, start_d), (x, &ch)| match ch {
                    '#' => {
                        obstacles.insert((x, y));
                        (obstacles, start_x, start_y, start_d)
                    }
                    '.' => (obstacles, start_x, start_y, start_d),
                    ch => (obstacles, x, y, Direction::from(ch)),
                },
            )
        },
    )
}

fn gallivant(
    pos: &mut Guard,
    obstacles: &HashSet<(usize, usize)>,
    height: usize,
    width: usize,
) -> Option<HashSet<(usize, usize)>> {
    let mut loop_counter = 0;
    let mut finished = false;
    let mut visits: HashSet<_> = [(pos.x, pos.y)].into();
    while !finished {
        match pos.d {
            Direction::North => {
                for y in (0..pos.y).rev() {
                    if obstacles.contains(&(pos.x, y)) {
                        pos.y = y + 1;
                        break;
                    } else {
                        if !visits.insert((pos.x, y)) {
                            loop_counter += 1;
                        } else {
                            loop_counter = 0;
                        }
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
                        break;
                    } else {
                        if !visits.insert((x, pos.y)) {
                            loop_counter += 1;
                        } else {
                            loop_counter = 0;
                        }
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
                        break;
                    } else {
                        if !visits.insert((pos.x, y)) {
                            loop_counter += 1;
                        } else {
                            loop_counter = 0;
                        }
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
                        break;
                    } else {
                        if !visits.insert((x, pos.y)) {
                            loop_counter += 1;
                        } else {
                            loop_counter = 0;
                        }
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

        pos.rotate();
    }

    Some(visits)
}
