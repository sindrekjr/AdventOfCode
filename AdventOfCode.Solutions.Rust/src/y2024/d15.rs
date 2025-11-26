use std::{
    collections::{HashMap, HashSet},
    iter::once,
};

use crate::{
    core::{Part, Solution},
    utils::grid::{parse_grid, Coordinate, Direction},
};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day15::solve_part_one(input),
        Part::P2 => Day15::solve_part_two(input),
    }
}

struct Day15;
impl Solution for Day15 {
    fn solve_part_one(input: String) -> Option<String> {
        let (map, moves) = input.split_once("\n\n").unwrap();

        let mut map = parse_grid(map);
        let mut robot = *map.iter().find(|(_, ch)| **ch == '@').unwrap().0;
        map.insert(robot, '.');

        for m in parse_moves(moves) {
            if let Some(coordinates) = try_move(&robot, &m, &map) {
                map.insert(robot, '.');

                let mut coordinates_vec: Vec<Coordinate> =
                    coordinates.clone().into_iter().collect();
                match m {
                    Direction::N => {
                        robot = robot.north();
                        coordinates_vec.sort_by_key(|coor| coor.y);

                        for coor in coordinates_vec {
                            let previous = coor.south();
                            if coordinates.contains(&previous) {
                                map.insert(coor, map[&previous]);
                                map.insert(previous, '.');
                            }
                        }
                    }
                    Direction::E => {
                        robot = robot.east();
                        coordinates_vec.sort_by_key(|coor| -coor.x);

                        for coor in coordinates_vec {
                            let previous = coor.west();
                            if coordinates.contains(&previous) {
                                map.insert(coor, map[&previous]);
                                map.insert(previous, '.');
                            }
                        }
                    }
                    Direction::W => {
                        robot = robot.west();
                        coordinates_vec.sort_by_key(|coor| coor.x);

                        for coor in coordinates_vec {
                            let previous = coor.east();
                            if coordinates.contains(&previous) {
                                map.insert(coor, map[&previous]);
                                map.insert(previous, '.');
                            }
                        }
                    }
                    Direction::S => {
                        robot = robot.south();
                        coordinates_vec.sort_by_key(|coor| -coor.y);

                        for coor in coordinates_vec {
                            let previous = coor.north();
                            if coordinates.contains(&previous) {
                                map.insert(coor, map[&previous]);
                                map.insert(previous, '.');
                            }
                        }
                    }
                };
            }
        }

        Some(
            map.iter()
                .filter_map(|(coor, ch)| {
                    if *ch == 'O' {
                        Some(coor.x + coor.y * 100)
                    } else {
                        None
                    }
                })
                .sum::<isize>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let (map, moves) = input.split_once("\n\n").unwrap();
        let map: String = map
            .chars()
            .map(|ch| match ch {
                '#' => "##",
                'O' => "[]",
                '.' => "..",
                '@' => "@.",
                '\n' => "\n",
                _ => panic!("illegal character in map"),
            })
            .collect();

        let mut map = parse_grid(&map);
        let mut robot = *map.iter().find(|(_, ch)| **ch == '@').unwrap().0;
        map.insert(robot, '.');

        for m in parse_moves(moves) {
            if let Some(coordinates) = try_move(&robot, &m, &map) {
                map.insert(robot, '.');

                let mut coordinates_vec: Vec<Coordinate> =
                    coordinates.clone().into_iter().collect();
                match m {
                    Direction::N => {
                        robot = robot.north();
                        coordinates_vec.sort_by_key(|coor| coor.y);

                        for coor in coordinates_vec {
                            let previous = coor.south();
                            if coordinates.contains(&previous) {
                                map.insert(coor, map[&previous]);
                                map.insert(previous, '.');
                            }
                        }
                    }
                    Direction::E => {
                        robot = robot.east();
                        coordinates_vec.sort_by_key(|coor| -coor.x);

                        for coor in coordinates_vec {
                            let previous = coor.west();
                            if coordinates.contains(&previous) {
                                map.insert(coor, map[&previous]);
                                map.insert(previous, '.');
                            }
                        }
                    }
                    Direction::W => {
                        robot = robot.west();
                        coordinates_vec.sort_by_key(|coor| coor.x);

                        for coor in coordinates_vec {
                            let previous = coor.east();
                            if coordinates.contains(&previous) {
                                map.insert(coor, map[&previous]);
                                map.insert(previous, '.');
                            }
                        }
                    }
                    Direction::S => {
                        robot = robot.south();
                        coordinates_vec.sort_by_key(|coor| -coor.y);

                        for coor in coordinates_vec {
                            let previous = coor.north();
                            if coordinates.contains(&previous) {
                                map.insert(coor, map[&previous]);
                                map.insert(previous, '.');
                            }
                        }
                    }
                };
            }
        }

        Some(
            map.iter()
                .filter_map(|(coor, ch)| {
                    if *ch == '[' || *ch == 'O' {
                        Some(coor.x + coor.y * 100)
                    } else {
                        None
                    }
                })
                .sum::<isize>()
                .to_string(),
        )
    }
}

fn parse_moves(moves: &str) -> Vec<Direction> {
    moves
        .chars()
        .filter_map(|m| match m {
            '^' => Some(Direction::N),
            '>' => Some(Direction::E),
            '<' => Some(Direction::W),
            'v' => Some(Direction::S),
            '\n' => None,
            _ => panic!("illegal move"),
        })
        .collect()
}

fn try_move(
    position: &Coordinate,
    direction: &Direction,
    map: &HashMap<Coordinate, char>,
) -> Option<HashSet<Coordinate>> {
    let next_position = match direction {
        Direction::N => position.north(),
        Direction::E => position.east(),
        Direction::W => position.west(),
        Direction::S => position.south(),
    };

    match map[&next_position] {
        '#' => None,
        '.' => Some([next_position].into()),
        'O' => match try_move(&next_position, direction, map) {
            Some(coordinates) => Some(coordinates.into_iter().chain(once(next_position)).collect()),
            None => None,
        },
        '[' | ']' if *direction == Direction::E || *direction == Direction::W => {
            match try_move(&next_position, direction, map) {
                Some(coordinates) => {
                    Some(coordinates.into_iter().chain(once(next_position)).collect())
                }
                None => None,
            }
        }
        '[' => match direction {
            Direction::N | Direction::S => {
                match (
                    try_move(&next_position, direction, map),
                    try_move(&next_position.east(), direction, map),
                ) {
                    (Some(coordinates_left), Some(coordinates_right)) => Some(
                        coordinates_left
                            .into_iter()
                            .chain(coordinates_right)
                            .chain([next_position, next_position.east()])
                            .collect(),
                    ),
                    _ => None,
                }
            }
            _ => None,
        },
        ']' => match direction {
            Direction::N | Direction::S => {
                match (
                    try_move(&next_position.west(), direction, map),
                    try_move(&next_position, direction, map),
                ) {
                    (Some(coordinates_left), Some(coordinates_right)) => Some(
                        coordinates_left
                            .into_iter()
                            .chain(coordinates_right)
                            .chain([next_position.west(), next_position])
                            .collect(),
                    ),
                    _ => None,
                }
            }
            _ => None,
        },
        _ => None,
    }
}
