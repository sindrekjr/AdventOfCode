use std::{
    collections::{HashMap, HashSet},
    iter::once,
};

use crate::{
    core::{Part, Solution},
    utils::grid::{parse_grid, Coordinate, Direction},
};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day15::solve_part_one(input),
        Part::P2 => Day15::solve_part_two(input),
    }
}

struct Day15;
impl Solution for Day15 {
    fn solve_part_one(input: String) -> String {
        let (map, moves) = input.split_once("\n\n").unwrap();

        let mut map = parse_grid(map);
        let mut robot = *map.iter().find(|(_, ch)| **ch == '@').unwrap().0;
        map.insert(robot, '.');

        for m in moves.chars() {
            let adj_pos = match m {
                '^' => robot.north(),
                '>' => robot.east(),
                '<' => robot.west(),
                'v' => robot.south(),
                _ => continue,
            };

            match map[&adj_pos] {
                '.' => robot = adj_pos,
                '#' => {}
                'O' => {
                    let mut next_pos = adj_pos;
                    while map[&next_pos] == 'O' {
                        next_pos = match m {
                            '^' => next_pos.north(),
                            '>' => next_pos.east(),
                            '<' => next_pos.west(),
                            'v' => next_pos.south(),
                            _ => panic!("illegal move"),
                        };
                    }

                    if map[&next_pos] == '.' {
                        map.insert(adj_pos, '.');
                        map.insert(next_pos, 'O');
                        robot = adj_pos;
                    }
                }
                _ => (),
            }
        }

        map.iter()
            .filter_map(|(coor, ch)| {
                if *ch == 'O' {
                    Some(coor.x + coor.y * 100)
                } else {
                    None
                }
            })
            .sum::<isize>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
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
        let moves = moves.chars().filter_map(|m| match m {
            '^' => Some(Direction::N),
            '>' => Some(Direction::E),
            '<' => Some(Direction::W),
            'v' => Some(Direction::S),
            '\n' => None,
            _ => panic!("illegal move"),
        });

        let mut map = parse_grid(&map);
        let mut robot = *map.iter().find(|(_, ch)| **ch == '@').unwrap().0;
        map.insert(robot, '.');

        for m in moves {
            if let Some(coordinates) = try_move(&robot, &m, &map) {
                map.insert(robot, '.');

                let mut coordinates_vec: Vec<Coordinate> =
                    coordinates.clone().into_iter().collect();
                match m {
                    Direction::N => {
                        robot = robot.north();
                        coordinates_vec.sort_by_key(|coor| coor.y);

                        for coor in coordinates_vec {
                            if coor.y == robot.y {
                                continue;
                            }

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
                            if coor.x == robot.x {
                                continue;
                            }

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
                            if coor.x == robot.x {
                                continue;
                            }

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
                            if coor.y == robot.y {
                                continue;
                            }

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

        map.iter()
            .filter_map(|(coor, ch)| {
                if *ch == '[' {
                    Some(coor.x + coor.y * 100)
                } else {
                    None
                }
            })
            .sum::<isize>()
            .to_string()
    }
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
            Direction::E | Direction::W => {
                if let Some(coordinates) = try_move(&next_position, direction, map) {
                    Some(coordinates.into_iter().chain(once(next_position)).collect())
                } else {
                    None
                }
            }
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
            Direction::E | Direction::W => {
                if let Some(coordinates) = try_move(&next_position, direction, map) {
                    Some(coordinates.into_iter().chain(once(next_position)).collect())
                } else {
                    None
                }
            }
        },
        _ => panic!("illegal object"),
    }
}
