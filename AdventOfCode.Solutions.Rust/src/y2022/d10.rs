use std::str::Lines;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day10::solve_part_one(input),
        Part::P2 => Day10::solve_part_two(input),
    }
}

struct Day10;
impl Solution for Day10 {
    fn solve_part_one(input: String) -> Option<String> {
        let cycles = parse_cycles(input.lines());

        Some(
            [20, 60, 100, 140, 180, 220]
                .iter()
                .map(|cycle| cycles[cycle - 1] as usize * cycle)
                .sum::<usize>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        Some(
            parse_cycles(input.lines())
                .iter()
                .enumerate()
                .map(|(c, x)| {
                    let horizontal = (c % 40) as i32;
                    let del = if horizontal == 0 { "\n" } else { "" };

                    let x_distance = x - horizontal;
                    if x_distance <= 1 && x_distance >= -1 {
                        format!("{}#", del)
                    } else {
                        format!("{}.", del)
                    }
                })
                .collect(),
        )
    }
}

fn parse_cycles(lines: Lines<'_>) -> Vec<i32> {
    let mut cycles = vec![1];
    for line in lines {
        if let Some((_, val)) = line.split_once(' ') {
            cycles.push(*cycles.last().unwrap());
            cycles.push(*cycles.last().unwrap() + val.parse::<i32>().unwrap());
        } else {
            cycles.push(*cycles.last().unwrap());
        }
    }

    cycles
}
