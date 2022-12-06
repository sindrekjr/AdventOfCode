use std::collections::HashSet;

use crate::core::{Solution, Part};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day06::solve_part_one(input),
        Part::P2 => Day06::solve_part_two(input),
    }
}

pub struct Day06 {}
impl Solution for Day06 {
    fn solve_part_one(input: String) -> String {
        let mut marker: Vec<char> = Vec::new();

        for (i, c) in input.char_indices() {
            marker.insert(0, c);
            if marker.len() == 5 {
                marker.pop();
            }

            let set: HashSet<char> = marker.clone().into_iter().collect();

            if set.len() == 4 {
                return (i + 1).to_string();
            }
        }

        String::new()
    }

    fn solve_part_two(input: String) -> String {
        let mut marker: Vec<char> = Vec::new();

        for (i, c) in input.char_indices() {
            marker.insert(0, c);
            if marker.len() == 15 {
                marker.pop();
            }

            let set: HashSet<char> = marker.clone().into_iter().collect();

            if set.len() == 14 {
                return (i + 1).to_string();
            }
        }

        String::new()
    }
}
