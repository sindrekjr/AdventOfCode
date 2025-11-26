use std::collections::{BTreeMap, HashMap};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day24::solve_part_one(input),
        Part::P2 => Day24::solve_part_two(input),
    }
}

enum Gate<'a> {
    Binary(&'a str, &'a str, &'a str),
    Value(bool),
}

impl<'a> From<&'a str> for Gate<'a> {
    fn from(value: &'a str) -> Self {
        let [a, op, b] = value
            .split_whitespace()
            .collect::<Vec<_>>()
            .try_into()
            .unwrap();
        Self::Binary(op, a, b)
    }
}

struct Day24;
impl Solution for Day24 {
    fn solve_part_one(input: String) -> Option<String> {
        let wires: BTreeMap<&str, Gate> = input
            .lines()
            .filter_map(|line| {
                if let Some((wire, input)) = line.split_once(": ") {
                    Some((wire, Gate::Value(input == "1")))
                } else if let Some((output, wire)) = line.split_once(" -> ") {
                    Some((wire, Gate::from(output)))
                } else {
                    None
                }
            })
            .collect();

        Some(
            wires
                .iter()
                .filter(|(k, _)| k.starts_with('z'))
                .enumerate()
                .fold(0 as u64, |val, (i, (key, _))| {
                    if show_me_the_money(key, &wires) {
                        val | (1 << i)
                    } else {
                        val
                    }
                })
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let mut output_chains = HashMap::new();
        let wires: BTreeMap<&str, Gate> = input
            .lines()
            .filter_map(|line| {
                if let Some((wire, input)) = line.split_once(": ") {
                    Some((wire, Gate::Value(input == "1")))
                } else if let Some((output, wire)) = line.split_once(" -> ") {
                    let gate = Gate::from(output);
                    if let Gate::Binary(_, a, b) = gate {
                        output_chains.entry(a).or_insert(vec![]).push(wire);
                        output_chains.entry(b).or_insert(vec![]).push(wire);
                    }

                    Some((wire, Gate::from(output)))
                } else {
                    None
                }
            })
            .collect();

        let max_z = wires.keys().filter(|k| k.starts_with('z')).max().unwrap();

        let mut faulty = wires
            .iter()
            .filter(|(&k, gate)| {
                let mut and_in_chain = false;
                let mut or_in_chain = false;
                let mut xor_in_chain = false;
                let mut chain_length = 0;
                if let Some(chain) = output_chains.get(k) {
                    chain_length = chain.len();
                    chain.iter().for_each(|&k| {
                        if let Gate::Binary(op, _, _) = wires[k] {
                            match op {
                                "AND" => and_in_chain = true,
                                "OR" => or_in_chain = true,
                                "XOR" => xor_in_chain = true,
                                _ => unreachable!(),
                            }
                        }
                    });
                } else {
                    and_in_chain = true;
                    xor_in_chain = true;
                }

                match gate {
                    Gate::Binary(op, a, _) => match *op {
                        "AND" => {
                            if *a == "x00" || *a == "y00" {
                                chain_length == 0
                                    || chain_length != 2
                                    || !and_in_chain
                                    || !xor_in_chain
                            } else {
                                chain_length == 0 || chain_length != 1 || !or_in_chain
                            }
                        }
                        "OR" => {
                            if chain_length == 0 {
                                k != *max_z
                            } else {
                                chain_length != 2 || !and_in_chain || !xor_in_chain
                            }
                        }
                        "XOR" => {
                            if (a.starts_with('x') || a.starts_with('y')) && !a.ends_with("00") {
                                chain_length == 0
                                    || chain_length != 2
                                    || !and_in_chain
                                    || !xor_in_chain
                            } else {
                                !k.starts_with('z')
                            }
                        }
                        _ => unreachable!(),
                    },
                    Gate::Value(_) => false,
                }
            })
            .map(|(&k, _)| k)
            .collect::<Vec<_>>();

        faulty.sort();
        Some(faulty.join(","))
    }
}

fn show_me_the_money(key: &str, wires: &BTreeMap<&str, Gate>) -> bool {
    match &wires[&key] {
        Gate::Binary(op, a, b) => match *op {
            "AND" => show_me_the_money(a, wires) & show_me_the_money(b, wires),
            "OR" => show_me_the_money(a, wires) | show_me_the_money(b, wires),
            "XOR" => show_me_the_money(a, wires) ^ show_me_the_money(b, wires),
            _ => unreachable!(),
        },
        Gate::Value(value) => *value,
    }
}
