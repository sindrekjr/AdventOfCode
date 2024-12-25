use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day07::solve_part_one(input),
        Part::P2 => Day07::solve_part_two(input),
    }
}

struct Day07;

#[allow(unused_variables)]
impl Solution for Day07 {
    fn solve_part_one(input: String) -> String {
        String::default()
    }

    fn solve_part_two(input: String) -> String {
        String::default()
    }
}
