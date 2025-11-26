use std::collections::HashSet;

use crate::{
    core::{Part, Solution},
    utils::grid::{parse_grid, Coordinate},
};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day20::solve_part_one(input),
        Part::P2 => Day20::solve_part_two(input),
    }
}

struct Day20;
impl Solution for Day20 {
    fn solve_part_one(input: String) -> Option<String> {
        let (start, walls) = parse_elements(&input);
        Some(black_sheep_wall(&start, &walls, 2, 100).to_string())
    }

    fn solve_part_two(input: String) -> Option<String> {
        let (start, walls) = parse_elements(&input);
        Some(black_sheep_wall(&start, &walls, 20, 100).to_string())
    }
}

fn parse_elements(input: &str) -> (Coordinate, HashSet<Coordinate>) {
    let map = parse_grid(input);
    let start = *map.iter().find(|(_, ch)| **ch == 'S').unwrap().0;
    let walls: HashSet<Coordinate> = map
        .iter()
        .filter_map(|(coor, ch)| match *ch == '#' {
            true => Some(*coor),
            false => None,
        })
        .collect();

    (start, walls)
}

fn black_sheep_wall(
    start: &Coordinate,
    walls: &HashSet<Coordinate>,
    picoseconds: isize,
    gain: usize,
) -> usize {
    let mut path: Vec<Coordinate> = vec![*start];

    let mut step = 0;
    loop {
        let position = path[step];
        let neighbours = position.neighbours_orthogonal();
        let next = neighbours.iter().find(|neighbour| {
            (step == 0 || **neighbour != path[step - 1]) && !walls.contains(&neighbour)
        });

        if let Some(position) = next {
            path.push(*position);
            step += 1;
        } else {
            break;
        }
    }

    let mut count = 0;
    for (step, position) in path.iter().enumerate() {
        if let Some(shortcuts) = path.get((step + gain)..) {
            count += shortcuts
                .iter()
                .enumerate()
                .filter(|(c, p)| {
                    let distance = position.manhattan_distance(p);
                    distance <= picoseconds && distance <= *c as isize
                })
                .count()
        }
    }

    count
}
