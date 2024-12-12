use std::collections::{HashMap, HashSet};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day12::solve_part_one(input),
        Part::P2 => Day12::solve_part_two(input),
    }
}

const DIRECTIONS: [(isize, isize); 4] = [(-1, 0), (1, 0), (0, -1), (0, 1)];

struct Day12;
impl Solution for Day12 {
    fn solve_part_one(input: String) -> String {
        let map = parse_map(&input);
        let mut visited: HashSet<(isize, isize)> = HashSet::new();

        map.iter()
            .map(|((x, y), plant)| {
                let (region, perimeter) = find_region((*x, *y, *plant), &map, &mut visited);
                region.len() * perimeter.len()
            })
            .sum::<usize>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        String::new()
    }
}

fn parse_map(input: &String) -> HashMap<(isize, isize), char> {
    input
        .lines()
        .enumerate()
        .flat_map(|(y, line)| {
            line.char_indices()
                .map(|(x, ch)| ((x as isize, y as isize), ch))
                .collect::<Vec<_>>()
        })
        .collect()
}

fn find_region(
    position: (isize, isize, char),
    map: &HashMap<(isize, isize), char>,
    visited: &mut HashSet<(isize, isize)>,
) -> (HashSet<(isize, isize)>, Vec<(isize, isize)>) {
    let (x, y, plant) = position;
    let mut region = HashSet::new();
    let mut perimeter = vec![];
    region.insert((x, y));

    if visited.contains(&(x, y)) {
        return (region, perimeter);
    } else {
        visited.insert((x, y));
    }

    DIRECTIONS.iter().for_each(|(x_d, y_d)| {
        let adjacent = ((x + x_d), (y + y_d));

        match map.get(&adjacent) {
            Some(adjacent_plant) if &plant == adjacent_plant => {
                let (sub_reg, sub_per) =
                    find_region((adjacent.0, adjacent.1, *adjacent_plant), map, visited);
                region.extend(sub_reg);
                perimeter.extend(sub_per);
            }
            _ => {
                perimeter.push(adjacent);
            }
        }
    });

    return (region, perimeter);
}
