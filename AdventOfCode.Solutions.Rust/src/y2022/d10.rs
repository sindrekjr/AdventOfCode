use crate::core::{Part, Solution};

pub fn solve(part: crate::core::Part, input: String) -> String {
    match part {
        Part::P1 => Day10::solve_part_one(input),
        Part::P2 => Day10::solve_part_two(input),
    }
}

struct Day10;
impl Solution for Day10 {
    fn solve_part_one(input: String) -> String {
        let mut signals = vec![1];

        for line in input.lines().map(|l| l.split_once(' ')) {
            if let Some((_, val)) = line {
                signals.push(*signals.last().unwrap());
                signals.push(*signals.last().unwrap() + val.parse::<i32>().unwrap());
            } else {
                signals.push(*signals.last().unwrap());
            }
        }

        [20, 60, 100, 140, 180, 220]
            .iter()
            .map(|cycle| signals[cycle - 1] as usize * cycle)
            .sum::<usize>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let mut cycles = vec![1];

        for line in input.lines().map(|l| l.split_once(' ')) {
            if let Some((_, val)) = line {
                cycles.push(*cycles.last().unwrap());
                cycles.push(*cycles.last().unwrap() + val.parse::<i32>().unwrap());
            } else {
                cycles.push(*cycles.last().unwrap());
            }
        }

        cycles.iter().enumerate().map(|(c, x)| {
            let del = if c % 40 == 0 {
                "\n"
            } else {
                ""
            };

            if (x - (c % 40) as i32).abs() <= 1 {
                format!("{}#", del)
            } else {
                format!("{}.", del)
            }
        }).collect()
    }
}
