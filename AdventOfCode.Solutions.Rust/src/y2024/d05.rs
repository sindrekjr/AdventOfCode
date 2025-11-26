use std::collections::HashSet;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day05::solve_part_one(input),
        Part::P2 => Day05::solve_part_two(input),
    }
}

struct Day05;
impl Solution for Day05 {
    fn solve_part_one(input: String) -> Option<String> {
        let (rules, updates) = input.split_once("\n\n").unwrap();

        let rules: HashSet<(u16, u16)> = rules
            .lines()
            .map(|line| {
                let (a, b) = line.split_once('|').unwrap();
                (a.parse().unwrap(), b.parse().unwrap())
            })
            .collect();

        Some(
            updates
                .lines()
                .filter_map(|line| {
                    let pages: Vec<u16> = line.split(',').map(|p| p.parse().unwrap()).collect();

                    if pages.is_sorted_by(|a, b| !rules.contains(&(*b, *a))) {
                        Some(pages[pages.len() / 2])
                    } else {
                        None
                    }
                })
                .sum::<u16>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let (rules, updates) = input.split_once("\n\n").unwrap();

        let rules: HashSet<(u16, u16)> = rules
            .lines()
            .map(|line| {
                let (a, b) = line.split_once('|').unwrap();
                (a.parse().unwrap(), b.parse().unwrap())
            })
            .collect();

        Some(
            updates
                .lines()
                .filter_map(|line| {
                    let mut pages: Vec<u16> = line.split(',').map(|p| p.parse().unwrap()).collect();

                    if pages.is_sorted_by(|a, b| !rules.contains(&(*b, *a))) {
                        None
                    } else {
                        pages.sort_by(|a, b| rules.contains(&(*a, *b)).cmp(&true));
                        Some(pages[pages.len() / 2])
                    }
                })
                .sum::<u16>()
                .to_string(),
        )
    }
}
