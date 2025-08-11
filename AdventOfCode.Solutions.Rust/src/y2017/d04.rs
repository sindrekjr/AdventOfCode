use std::collections::HashSet;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day04::solve_part_one(input),
        Part::P2 => Day04::solve_part_two(input),
    }
}

struct Day04;

#[allow(unused_variables)]
impl Solution for Day04 {
    fn solve_part_one(input: String) -> String {
        input
            .lines()
            .filter(|line| {
                let split = line.split_whitespace();
                split.clone().collect::<HashSet<&str>>().len() == split.count()
            })
            .count()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        input
            .lines()
            .filter(|line| {
                let split = line.split_whitespace();
                split
                    .clone()
                    .map(|str| {
                        let mut chars = str.chars().collect::<Vec<char>>();
                        chars.sort();
                        chars
                    })
                    .collect::<HashSet<Vec<char>>>()
                    .len()
                    == split.count()
            })
            .count()
            .to_string()
    }
}
