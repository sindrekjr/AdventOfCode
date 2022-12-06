// use super::Day;

use crate::{
    core::{Part, Solution},
    utils::string::StrUtils,
};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day01::solve_part_one(input),
        Part::P2 => Day01::solve_part_two(input),
    }
}

pub struct Day01;
impl Solution for Day01 {
    fn solve_part_one(input: String) -> String {
        parse_calorie_totals(&input).max().unwrap().to_string()
    }

    fn solve_part_two(input: String) -> String {
        let mut elves = parse_calorie_totals(&input).collect::<Vec<u32>>();

        elves.sort();
        elves.reverse();
        elves.into_iter().take(3).sum::<u32>().to_string()
    }
}

fn parse_calorie_totals<'a>(input: &'a str) -> impl Iterator<Item = u32> + 'a {
    input
        .paragraphs()
        .map(|elf| elf.lines().filter_map(|c| c.parse::<u32>().ok()).sum())
}
