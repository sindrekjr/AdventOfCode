use std::collections::HashMap;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day07::solve_part_one(input),
        Part::P2 => Day07::solve_part_two(input),
    }
}

struct Day07;
impl Solution for Day07 {
    fn solve_part_one(input: String) -> Option<String> {
        let mut operators_cache = HashMap::new();

        Some(
            parse_input(input)
                .iter()
                .filter_map(|(value, numbers)| {
                    if numbers.len() == 1 {
                        if *value == numbers[0] {
                            return Some(value);
                        } else {
                            return None;
                        }
                    }

                    for operators in get_operators(numbers.len() - 1, &mut operators_cache, false) {
                        let mut result = numbers[0];
                        for (i, op) in operators.iter().enumerate() {
                            match op {
                                '+' => result += numbers[i + 1],
                                '*' => result *= numbers[i + 1],
                                _ => panic!(),
                            }
                        }

                        if result == *value {
                            return Some(value);
                        }
                    }

                    None
                })
                .sum::<u64>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let mut operators_cache = HashMap::new();

        Some(
            parse_input(input)
                .iter()
                .filter_map(|(value, numbers)| {
                    if numbers.len() == 1 {
                        if *value == numbers[0] {
                            return Some(value);
                        } else {
                            return None;
                        }
                    }

                    for operators in get_operators(numbers.len() - 1, &mut operators_cache, true) {
                        let mut result = numbers[0];
                        for (i, op) in operators.iter().enumerate() {
                            match op {
                                '+' => result += numbers[i + 1],
                                '*' => result *= numbers[i + 1],
                                '|' => {
                                    result =
                                        format!("{}{}", result, numbers[i + 1]).parse().unwrap()
                                }
                                _ => panic!(),
                            }
                        }

                        if result == *value {
                            return Some(value);
                        }
                    }

                    None
                })
                .sum::<u64>()
                .to_string(),
        )
    }
}

const OPERATORS: [char; 2] = ['+', '*'];
const CONCATENATION_OPERATOR: char = '|';

fn get_operators(
    length: usize,
    cache: &mut HashMap<usize, Vec<Vec<char>>>,
    include_concat: bool,
) -> Vec<Vec<char>> {
    if let Some(operators) = cache.get(&length) {
        return operators.clone();
    }

    let mut known_operators = OPERATORS.to_vec();
    if include_concat {
        known_operators.push(CONCATENATION_OPERATOR);
    }

    let operators: Vec<Vec<char>> = if length == 1 {
        known_operators.iter().map(|&op| vec![op]).collect()
    } else {
        let sub_operators = get_operators(length - 1, cache, include_concat);
        sub_operators
            .into_iter()
            .flat_map(|sub| {
                known_operators.iter().map(move |&op| {
                    let mut new_sub = sub.clone();
                    new_sub.push(op);
                    new_sub
                })
            })
            .collect()
    };

    cache.insert(length, operators.clone());

    operators
}

fn parse_input(input: String) -> Vec<(u64, Vec<u64>)> {
    input
        .lines()
        .map(|line| {
            let (value, numbers) = line.split_once(':').unwrap();

            let value = value.parse().unwrap();
            let numbers = numbers
                .split_whitespace()
                .map(|num| num.parse().unwrap())
                .collect();

            (value, numbers)
        })
        .collect()
}
