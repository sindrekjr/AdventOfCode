use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day01::solve_part_one(input),
        Part::P2 => Day01::solve_part_two(input),
    }
}

struct Day01;
impl Solution for Day01 {
    fn solve_part_one(input: String) -> Option<String> {
        let mut list1: Vec<i32> = vec![];
        let mut list2: Vec<i32> = vec![];

        input.lines().for_each(|l| {
            let mut parts = l.split_whitespace();
            if let (Some(a), Some(b)) = (parts.next(), parts.next()) {
                list1.push(a.parse::<i32>().unwrap());
                list2.push(b.parse::<i32>().unwrap());
            }
        });

        list1.sort();
        list2.sort();

        Some(
            list1
                .iter()
                .zip(list2.iter())
                .map(|(a, b)| (a - b).abs())
                .sum::<i32>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let mut list1: Vec<i32> = vec![];
        let mut list2: Vec<i32> = vec![];

        input.lines().for_each(|l| {
            let mut parts = l.split_whitespace();
            if let (Some(a), Some(b)) = (parts.next(), parts.next()) {
                list1.push(a.parse::<i32>().unwrap());
                list2.push(b.parse::<i32>().unwrap());
            }
        });

        Some(
            list1
                .iter()
                .map(|a| a * list2.iter().filter(|&b| b == a).count() as i32)
                .sum::<i32>()
                .to_string(),
        )
    }
}
