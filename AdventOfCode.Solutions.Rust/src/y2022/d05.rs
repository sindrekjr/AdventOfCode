use crate::core::Solution;

use super::Part;

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day05::solve_part_one(input),
        Part::P2 => Day05::solve_part_two(input),
    }
}

pub struct Day05 {}
impl Solution for Day05 {
    fn solve_part_one(input: String) -> String {
        let paragraphs = input.split("\n\n").collect::<Vec<_>>();
        let mut stacks = parse_stacks(paragraphs[0]);

        for instruction in paragraphs[1].lines() {
            let (count, from, to) = parse_moves(instruction);
            let old_stack = &mut stacks[from];
            let items = old_stack.split_off(old_stack.len() - count);
            let new_stack = &mut stacks[to];
            stack_crates(new_stack, items, 9000);
        }

        peek_top_crates(&stacks)
    }

    fn solve_part_two(input: String) -> String {
        let paragraphs = input.split("\n\n").collect::<Vec<_>>();
        let mut stacks = parse_stacks(paragraphs[0]);

        for instruction in paragraphs[1].lines() {
            let (count, from, to) = parse_moves(instruction);
            let old_stack = &mut stacks[from];
            let items = old_stack.split_off(old_stack.len() - count);
            let new_stack = &mut stacks[to];
            stack_crates(new_stack, items, 9001);
        }

        peek_top_crates(&stacks)
    }
}

fn parse_moves(instruction: &str) -> (usize, usize, usize) {
    let moves: Vec<usize> = instruction
        .split_whitespace()
        .filter_map(|w| match w.parse::<usize>() {
            Ok(n) => Some(n),
            Err(_) => None,
        })
        .collect();

    (moves[0], moves[1] - 1, moves[2] - 1)
}

fn parse_stacks(crates: &str) -> Vec<Vec<char>> {
    let mut first = true;
    let mut stacks = Vec::new();
    for line in crates.lines().rev() {
        if first {
            first = false;
            for _ in line.chars().enumerate().filter(|&(i, _)| i % 4 == 1) {
                stacks.push(Vec::new());
            }
        } else {
            let chars: Vec<char> = line
                .chars()
                .enumerate()
                .filter(|&(i, _)| i % 4 == 1)
                .map(|(_, v)| v)
                .collect();

            for (i, c) in chars.into_iter().enumerate() {
                if !c.is_whitespace() {
                    stacks[i].push(c);
                }
            }
        }
    }

    stacks
}

fn stack_crates(stack: &mut Vec<char>, crates: Vec<char>, model: u32) -> &mut Vec<char> {
    match model {
        9000 => {
            for item in crates.iter().rev() {
                stack.push(item.to_owned());
            }
            stack
        }
        9001 => {
            for item in crates {
                stack.push(item);
            }
            stack
        },
        _ => stack
    }
}

fn peek_top_crates(stacks: &Vec<Vec<char>>) -> String {
    stacks
        .into_iter()
        .map(|stack| stack.last().unwrap())
        .collect()
}
