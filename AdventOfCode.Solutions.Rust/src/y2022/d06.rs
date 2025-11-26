use std::collections::HashSet;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day06::solve_part_one(input),
        Part::P2 => Day06::solve_part_two(input),
    }
}

pub struct Day06;
impl Solution for Day06 {
    fn solve_part_one(input: String) -> Option<String> {
        Some(find_distinct_window(&input, 4).to_string())
    }

    fn solve_part_two(input: String) -> Option<String> {
        Some(find_distinct_window(&input, 14).to_string())
    }
}

fn find_distinct_window(input: &str, size: usize) -> usize {
    size + input
        .char_indices()
        .map(move |(from, _)| {
            input
                .char_indices()
                .skip(size - 1)
                .next()
                .map(|(to, c)| &input[from..from + to + c.len_utf8()])
                .unwrap()
                .chars()
                .collect::<HashSet<char>>()
        })
        .position(|set| set.len() == size)
        .unwrap()
}
