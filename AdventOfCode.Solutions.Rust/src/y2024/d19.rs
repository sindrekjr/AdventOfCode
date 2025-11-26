use std::collections::HashMap;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day19::solve_part_one(input),
        Part::P2 => Day19::solve_part_two(input),
    }
}

struct Day19;
impl Solution for Day19 {
    fn solve_part_one(input: String) -> Option<String> {
        let (towels, designs) = input.split_once("\n\n").unwrap();
        let towels: Vec<&str> = towels.split(", ").collect();
        let designs: Vec<_> = designs.lines().collect();

        let mut memo: HashMap<String, bool> = HashMap::new();
        Some(
            designs
                .iter()
                .filter(|design| possible(design.to_string(), &towels, &mut memo))
                .count()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let (towels, designs) = input.split_once("\n\n").unwrap();
        let towels: Vec<&str> = towels.split(", ").collect();
        let designs: Vec<_> = designs.lines().collect();

        let mut memo: HashMap<String, u64> = HashMap::new();
        Some(
            designs
                .iter()
                .map(|design| combinations(design.to_string(), &towels, &mut memo))
                .sum::<u64>()
                .to_string(),
        )
    }
}

fn possible(design: String, towels: &[&str], memo: &mut HashMap<String, bool>) -> bool {
    if let Some(maybe) = memo.get(&design) {
        return *maybe;
    }

    for towel in towels {
        if &design == towel
            || (design.starts_with(towel)
                && possible(design[towel.len()..].to_string(), towels, memo))
        {
            memo.insert(design, true);
            return true;
        }
    }

    memo.insert(design, false);
    false
}

fn combinations(design: String, towels: &[&str], memo: &mut HashMap<String, u64>) -> u64 {
    if let Some(count) = memo.get(&design) {
        return *count;
    }

    let mut count = 0;

    for towel in towels {
        if &design == towel {
            count += 1;
            memo.insert(design.clone(), count);
        } else if design.starts_with(towel) {
            count += combinations(design[towel.len()..].to_string(), towels, memo);
            memo.insert(design.clone(), count);
        }
    }

    memo.insert(design.clone(), count);
    count
}
