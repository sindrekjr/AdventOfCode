use std::collections::{HashMap, HashSet};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day10::solve_part_one(input),
        Part::P2 => Day10::solve_part_two(input),
    }
}

struct Day10;
impl Solution for Day10 {
    fn solve_part_one(input: String) -> String {
        let map = parse_map(&input);
        map.iter()
            .filter_map(|(&trailhead, &h)| {
                if h > 0 {
                    None
                } else {
                    Some(hike(&trailhead, &map).len())
                }
            })
            .sum::<usize>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let map = parse_map(&input);
        map.iter()
            .filter_map(|(&trailhead, &h)| {
                if h > 0 {
                    None
                } else {
                    Some(trail_rating(&trailhead, &map))
                }
            })
            .sum::<u32>()
            .to_string()
    }
}

const DIRECTIONS: [(isize, isize); 4] = [(-1, 0), (1, 0), (0, -1), (0, 1)];

fn parse_map(input: &String) -> HashMap<(usize, usize), u8> {
    input
        .lines()
        .enumerate()
        .flat_map(|(y, line)| {
            line.char_indices()
                .map(|(x, ch)| ((x, y), ch.to_digit(10).unwrap() as u8))
                .collect::<Vec<_>>()
        })
        .collect()
}

fn hike(position: &(usize, usize), map: &HashMap<(usize, usize), u8>) -> HashSet<(usize, usize)> {
    let current_step = map[position];
    if current_step == 9 {
        return [*position].into();
    }

    let next_step = current_step + 1;
    let (x, y) = position;

    DIRECTIONS
        .iter()
        .flat_map(|(x_d, y_d)| {
            let next_position = ((*x as isize + x_d) as usize, (*y as isize + y_d) as usize);

            match map.get(&next_position) {
                Some(&height) if height == next_step => hike(&next_position, &map),
                Some(_) => [].into(),
                None => [].into(),
            }
        })
        .collect()
}

fn trail_rating(position: &(usize, usize), map: &HashMap<(usize, usize), u8>) -> u32 {
    let current_step = map[position];
    if current_step == 9 {
        return 1;
    }

    let next_step = current_step + 1;
    let (x, y) = position;

    DIRECTIONS
        .iter()
        .map(|(x_d, y_d)| {
            let next_position = ((*x as isize + x_d) as usize, (*y as isize + y_d) as usize);

            match map.get(&next_position) {
                Some(&height) if height == next_step => trail_rating(&next_position, &map),
                Some(_) => 0,
                None => 0,
            }
        })
        .sum()
}
